using System.Net;
using System.Runtime.CompilerServices;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Shipyards;
using Wolfe.SpaceTraders.Infrastructure.Api;
using Wolfe.SpaceTraders.Infrastructure.Data;
using Wolfe.SpaceTraders.Sdk;
using Wolfe.SpaceTraders.Sdk.Models.Systems;

namespace Wolfe.SpaceTraders.Infrastructure.Exploration;

internal class SpaceTradersExplorationService(
    ISpaceTradersApiClient apiClient,
    ISpaceTradersDataClient dataClient
) : IExplorationService
{
    public async Task<Shipyard?> GetShipyard(WaypointId shipyardId, CancellationToken cancellationToken = default)
    {
        var cached = await dataClient.GetShipyard(shipyardId, cancellationToken);
        if (cached != null)
        {
            return cached.Item;
        }

        var waypoint = await GetWaypoint(shipyardId, cancellationToken);
        if (waypoint == null) { return null; }
        var response = await apiClient.GetShipyard(shipyardId.SystemId.Value, shipyardId.Value, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
        var shipyard = response.GetContent().Data.ToDomain(waypoint);

        await dataClient.AddShipyard(shipyard, cancellationToken);
        return shipyard;
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
            if (!waypoint.HasShipyard)
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
        if (cached != null) { return cached; }

        var response = await apiClient.GetWaypoint(waypointId.SystemId.Value, waypointId.Value, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
        var waypoint = response.GetContent().Data.ToDomain();

        await dataClient.AddWaypoint(waypoint, cancellationToken);
        return waypoint;
    }

    public async IAsyncEnumerable<Waypoint> GetWaypoints(SystemId systemId, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var cached = dataClient.GetWaypoints(systemId, cancellationToken);
        var hasCache = false;
        if (cached != null)
        {
            await foreach (var waypoint in cached)
            {
                hasCache = true;
                yield return waypoint;
            }

            // We've found at least one cached waypoint, so we can stop here.
            if (hasCache) { yield break; }
        }

        // We didn't find any cached waypoints, so we'll need to fetch them from the API.
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