using System.Net;
using System.Runtime.CompilerServices;
using Wolfe.SpaceTraders.Domain.Marketplace;
using Wolfe.SpaceTraders.Domain.Shipyards;
using Wolfe.SpaceTraders.Domain.Systems;
using Wolfe.SpaceTraders.Domain.Waypoints;
using Wolfe.SpaceTraders.Infrastructure.Api;
using Wolfe.SpaceTraders.Infrastructure.Api.Extensions;
using Wolfe.SpaceTraders.Infrastructure.Data;
using Wolfe.SpaceTraders.Sdk;
using Wolfe.SpaceTraders.Sdk.Models.Systems;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Infrastructure;

internal class SpaceTradersExplorationService(
    ISpaceTradersApiClient apiClient,
    ISpaceTradersDataClient dataClient
) : IExplorationService
{
    public async Task<Shipyard?> GetShipyard(WaypointId waypointId, CancellationToken cancellationToken = default)
    {
        var waypoint = await GetWaypoint(waypointId, cancellationToken);
        if (waypoint == null) { return null; }
        var response = await apiClient.GetShipyard(waypointId.System.Value, waypointId.Value, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
        return response.GetContent().Data.ToDomain(waypoint);
    }

    public async Task<Marketplace?> GetMarketplace(WaypointId waypointId, CancellationToken cancellationToken = default)
    {
        var cached = await dataClient.GetMarketplace(waypointId, cancellationToken);
        if (cached != null)
        {
            return cached.Item;
        }

        var waypoint = await GetWaypoint(waypointId, cancellationToken);
        if (waypoint == null) { return null; }
        var response = await apiClient.GetMarketplace(waypointId.System.Value, waypointId.Value, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
        var market = response.GetContent().Data.ToDomain(waypoint);

        await dataClient.AddMarketplace(market, cancellationToken);
        return market;
    }

    public async IAsyncEnumerable<Marketplace> GetMarketplaces(SystemId systemId, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var cached = dataClient.GetMarketplaces(systemId, cancellationToken);
        if (cached != null)
        {
            await foreach (var marketplace in cached)
            {
                yield return marketplace.Item;
            }
            yield break;
        }

        var waypoints = GetWaypoints(systemId, cancellationToken);
        await foreach (var waypoint in waypoints)
        {
            if (!waypoint.HasTrait(WaypointTraitId.Marketplace))
            {
                continue;
            }

            var market = await GetMarketplace(waypoint.Id, cancellationToken);
            if (market == null) { continue; }

            await dataClient.AddMarketplace(market, cancellationToken);
            yield return market;
        }
    }

    public async Task<StarSystem?> GetSystem(SystemId systemId, CancellationToken cancellationToken = default)
    {
        var response = await apiClient.GetSystem(systemId.Value, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
        return response.GetContent().Data.ToDomain();
    }

    public IAsyncEnumerable<StarSystem> GetSystems(CancellationToken cancellationToken = default)
    {
        return PaginationHelpers.ToAsyncEnumerable<SpaceTradersSystem>(
            async p => (await apiClient.GetSystems(20, p, cancellationToken)).GetContent()
        ).SelectAwait(s => ValueTask.FromResult(s.ToDomain()));
    }

    public async Task<Waypoint?> GetWaypoint(WaypointId waypointId, CancellationToken cancellationToken = default)
    {
        var cached = await dataClient.GetWaypoint(waypointId, cancellationToken);
        if (cached != null)
        {
            return cached.Item;
        }

        var response = await apiClient.GetWaypoint(waypointId.System.Value, waypointId.Value, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
        var waypoint = response.GetContent().Data.ToDomain();

        await dataClient.AddWaypoint(waypoint, cancellationToken);
        return waypoint;
    }

    public async IAsyncEnumerable<Waypoint> GetWaypoints(SystemId systemId, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var cached = dataClient.GetWaypoints(systemId, cancellationToken);
        if (cached != null)
        {
            await foreach (var waypoint in cached)
            {
                yield return waypoint.Item;
            }
            yield break;
        }

        var waypoints = await PaginationHelpers.ToAsyncEnumerable<SpaceTradersWaypoint>(
            async p => (await apiClient.GetWaypoints(systemId.Value, null, [], 20, p, cancellationToken)).GetContent()
        ).SelectAwait(s => ValueTask.FromResult(s.ToDomain()))
        .ToListAsync(cancellationToken);

        foreach (var waypoint in waypoints)
        {
            await dataClient.AddWaypoint(waypoint, cancellationToken);
            yield return waypoint;
        }
    }
}