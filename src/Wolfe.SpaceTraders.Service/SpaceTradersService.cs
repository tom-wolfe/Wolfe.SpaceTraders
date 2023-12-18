using Wolfe.SpaceTraders.Core.Models;
using Wolfe.SpaceTraders.Core.Requests;
using Wolfe.SpaceTraders.Core.Responses;

namespace Wolfe.SpaceTraders.Service;

public class SpaceTradersService : ISpaceTradersService
{
    private readonly ISpaceTradersClient _client;

    public SpaceTradersService(ISpaceTradersClient client)
    {
        _client = client;
    }

    public async Task<PurchaseShipResponse> PurchaseFirstShip(CancellationToken cancellationToken = default)
    {
        var agent = await _client.GetAgent(cancellationToken);
        var system = agent.Headquarters.SystemSymbol;

        var waypoints = _client.GetWaypoints(system, cancellationToken);

        await foreach (var waypoint in waypoints)
        {
            if (waypoint.Type != WaypointType.Shipyard && waypoint.Type != WaypointType.OrbitalStation)
            {
                continue;
            }

            var request = new PurchaseShipRequest
            {
                WaypointSymbol = waypoint.Symbol,
                ShipType = ShipType.MiningDrone
            };
            var response = await _client.PurchaseShip(request, cancellationToken);
            return response;
        }
        throw new Exception($"No shipyards found in system '{system}'.");
    }
}