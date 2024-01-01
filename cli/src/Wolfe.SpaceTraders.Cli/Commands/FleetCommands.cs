using Cocona;
using Microsoft.Extensions.Hosting;
using Wolfe.SpaceTraders.Cli.Formatters;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Fleet;
using Wolfe.SpaceTraders.Domain.Fleet.Commands;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Cli.Commands;

internal class FleetCommands(IFleetService fleetService, IHostApplicationLifetime host)
{
    public async Task<int> Purchase([Argument] WaypointId shipyardId, [Argument] ShipType shipType)
    {
        var request = new PurchaseShipCommand
        {
            ShipType = shipType,
            ShipyardId = shipyardId
        };
        await fleetService.PurchaseShip(request, host.ApplicationStopping);
        ConsoleHelpers.WriteLineSuccess($"Purchased ship successfully.");

        return ExitCodes.Success;
    }
}
