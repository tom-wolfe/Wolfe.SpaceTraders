using System.Net;
using System.Runtime.CompilerServices;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Marketplace;
using Wolfe.SpaceTraders.Domain.Shipyards;
using Wolfe.SpaceTraders.Infrastructure.Api;
using Wolfe.SpaceTraders.Infrastructure.Api.Extensions;
using Wolfe.SpaceTraders.Infrastructure.Data;
using Wolfe.SpaceTraders.Infrastructure.Data.Mapping;
using Wolfe.SpaceTraders.Sdk;
using Wolfe.SpaceTraders.Sdk.Models.Systems;

namespace Wolfe.SpaceTraders.Infrastructure;

internal class SpaceTradersExplorationService(
    ISpaceTradersApiClient apiClient,
    ISpaceTradersDataClient dataClient
) : IExplorationService
{
    public async Task<Marketplace?> GetMarketplace(WaypointId marketplaceId, CancellationToken cancellationToken = default)
    {
        var cached = await dataClient.GetMarketplace(marketplaceId, cancellationToken);
        if (cached != null)
        {
            return cached.Item;
        }

        var waypoint = await GetWaypoint(marketplaceId, cancellationToken);
        if (waypoint == null) { return null; }
        var response = await apiClient.GetMarketplace(marketplaceId.System.Value, marketplaceId.Value, cancellationToken);
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
            if (!waypoint.IsMarketplace)
            {
                continue;
            }

            var market = await GetMarketplace(waypoint.Id, cancellationToken);
            if (market == null) { continue; }

            await dataClient.AddMarketplace(market, cancellationToken);
            yield return market;
        }
    }

    public async Task<Shipyard?> GetShipyard(WaypointId shipyardId, CancellationToken cancellationToken = default)
    {
        var waypoint = await GetWaypoint(shipyardId, cancellationToken);
        if (waypoint == null) { return null; }
        var response = await apiClient.GetShipyard(shipyardId.System.Value, shipyardId.Value, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
        return response.GetContent().Data.ToDomain(waypoint);
    }

    public async IAsyncEnumerable<Shipyard> GetShipyards(SystemId systemId, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var cached = dataClient.GetShipyards(systemId, cancellationToken);
        if (cached != null)
        {
            await foreach (var shipyard in cached)
            {
                yield return shipyard.Item;
            }
            yield break;
        }

        var waypoints = GetWaypoints(systemId, cancellationToken);
        await foreach (var waypoint in waypoints)
        {
            if (!waypoint.IsShipyard)
            {
                continue;
            }

            var shipyard = await GetShipyard(waypoint.Id, cancellationToken);
            if (shipyard == null) { continue; }

            await dataClient.AddShipyard(shipyard, cancellationToken);
            yield return shipyard;
        }
    }

    public async Task<StarSystem?> GetSystem(SystemId systemId, CancellationToken cancellationToken = default)
    {
        var cached = await dataClient.GetSystem(systemId, cancellationToken);
        if (cached != null)
        {
            return cached.Item;
        }

        var response = await apiClient.GetSystem(systemId.Value, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
        var system = response.GetContent().Data.ToDomain();

        await dataClient.AddSystem(system, cancellationToken);
        return system;
    }

    public async IAsyncEnumerable<StarSystem> GetSystems([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var cached = dataClient.GetSystems(cancellationToken);
        if (cached != null)
        {
            await foreach (var system in cached)
            {
                yield return system.Item;
            }
            yield break;
        }

        var systems = PaginationHelpers.ToAsyncEnumerable<SpaceTradersSystem>(
            async p => (await apiClient.GetSystems(20, p, cancellationToken)).GetContent()
        ).SelectAwait(s => ValueTask.FromResult(s.ToDomain()));

        await foreach (var system in systems)
        {
            await dataClient.AddSystem(system, cancellationToken);
            yield return system;
        }
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

        var waypoints = PaginationHelpers.ToAsyncEnumerable<SpaceTradersWaypoint>(
            async p => (await apiClient.GetWaypoints(systemId.Value, 20, p, cancellationToken)).GetContent()
        ).SelectAwait(s => ValueTask.FromResult(s.ToDomain()));

        await foreach (var waypoint in waypoints)
        {
            await dataClient.AddWaypoint(waypoint, cancellationToken);
            yield return waypoint;
        }
    }
}