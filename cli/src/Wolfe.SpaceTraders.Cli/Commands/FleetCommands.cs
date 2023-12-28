using Cocona;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain.Fleet;
using Wolfe.SpaceTraders.Domain.Fleet.Commands;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Cli.Commands;

internal class FleetCommands(IFleetService fleetService)
{
    public async Task<int> Purchase([Argument] WaypointId shipyardId, [Argument] ShipType shipType, CancellationToken cancellationToken = default)
    {
        var request = new PurchaseShipCommand
        {
            ShipType = shipType,
            ShipyardId = shipyardId
        };
        await fleetService.PurchaseShip(request, cancellationToken);
        Console.WriteLine("Purchased ship successfully".Color(ConsoleColors.Success));

        return ExitCodes.Success;
    }
}
