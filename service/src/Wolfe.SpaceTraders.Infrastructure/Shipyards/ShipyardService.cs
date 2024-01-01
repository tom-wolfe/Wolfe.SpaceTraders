using System.Net;
using System.Runtime.CompilerServices;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Shipyards;
using Wolfe.SpaceTraders.Infrastructure.Api;
using Wolfe.SpaceTraders.Sdk;

namespace Wolfe.SpaceTraders.Infrastructure.Shipyards;

internal class ShipyardService(
    ISpaceTradersApiClient apiClient,
    IShipyardStore shipyardStore,
    IExplorationService explorationService
) : IShipyardService
{
    public async Task<Shipyard?> GetShipyard(WaypointId shipyardId, CancellationToken cancellationToken = default)
    {
        var cached = await shipyardStore.GetShipyard(shipyardId, cancellationToken);
        if (cached != null) { return cached; }

        var waypoint = await explorationService.GetWaypoint(shipyardId, cancellationToken);
        if (waypoint == null) { return null; }
        var response = await apiClient.GetShipyard(shipyardId.SystemId.Value, shipyardId.Value, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
        var shipyard = response.GetContent().Data.ToDomain(waypoint);

        await shipyardStore.AddShipyard(shipyard, cancellationToken);
        return shipyard;
    }

    public async IAsyncEnumerable<Shipyard> GetShipyards(SystemId systemId, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var cached = shipyardStore.GetShipyards(systemId, cancellationToken);
        var hasCached = false;

        await foreach (var shipyard in cached)
        {
            hasCached = true;
            yield return shipyard;
        }
        if (hasCached) { yield break; }

        var waypoints = explorationService.GetWaypoints(systemId, cancellationToken);
        await foreach (var waypoint in waypoints)
        {
            if (!waypoint.HasShipyard) { continue; }

            var shipyard = await GetShipyard(waypoint.Id, cancellationToken);
            if (shipyard == null) { continue; }

            yield return shipyard;
        }
    }
}