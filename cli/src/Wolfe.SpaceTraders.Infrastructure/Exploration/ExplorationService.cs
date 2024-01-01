using System.Net;
using System.Runtime.CompilerServices;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Infrastructure.Api;
using Wolfe.SpaceTraders.Sdk;
using Wolfe.SpaceTraders.Sdk.Models.Systems;

namespace Wolfe.SpaceTraders.Infrastructure.Exploration;

internal class ExplorationService(
    ISpaceTradersApiClient apiClient,
    IExplorationStore explorationStore
) : IExplorationService
{
    public async Task<StarSystem?> GetSystem(SystemId systemId, CancellationToken cancellationToken = default)
    {
        var cached = await explorationStore.GetSystem(systemId, cancellationToken);
        if (cached != null) { return cached; }

        var response = await apiClient.GetSystem(systemId.Value, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
        var system = response.GetContent().Data.ToDomain();

        await explorationStore.AddSystem(system, cancellationToken);
        return system;
    }

    public async IAsyncEnumerable<StarSystem> GetSystems([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var cached = explorationStore.GetSystems(cancellationToken);
        var hasCache = false;

        await foreach (var system in cached)
        {
            hasCache = true;
            yield return system;
        }
        if (hasCache) { yield break; }


        var systems = PaginationHelpers.ToAsyncEnumerable<SpaceTradersSystem>(
            async p => (await apiClient.GetSystems(20, p, cancellationToken)).GetContent()
        ).SelectAwait(s => ValueTask.FromResult(s.ToDomain()));

        await foreach (var system in systems)
        {
            await explorationStore.AddSystem(system, cancellationToken);
            yield return system;
        }
    }

    public async Task<Waypoint?> GetWaypoint(WaypointId waypointId, CancellationToken cancellationToken = default)
    {
        var cached = await explorationStore.GetWaypoint(waypointId, cancellationToken);
        if (cached != null) { return cached; }

        var response = await apiClient.GetWaypoint(waypointId.SystemId.Value, waypointId.Value, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
        var waypoint = response.GetContent().Data.ToDomain();

        await explorationStore.AddWaypoint(waypoint, cancellationToken);
        return waypoint;
    }

    public async IAsyncEnumerable<Waypoint> GetWaypoints(SystemId systemId, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var cached = explorationStore.GetWaypoints(systemId, cancellationToken);
        var hasCache = false;

        await foreach (var waypoint in cached)
        {
            hasCache = true;
            yield return waypoint;
        }
        if (hasCache) { yield break; }

        // We didn't find any cached waypoints, so we'll need to fetch them from the API.
        var waypoints = PaginationHelpers.ToAsyncEnumerable<SpaceTradersWaypoint>(
            async p => (await apiClient.GetWaypoints(systemId.Value, 20, p, cancellationToken)).GetContent()
        ).SelectAwait(s => ValueTask.FromResult(s.ToDomain()));

        await foreach (var waypoint in waypoints)
        {
            await explorationStore.AddWaypoint(waypoint, cancellationToken);
            yield return waypoint;
        }
    }
}