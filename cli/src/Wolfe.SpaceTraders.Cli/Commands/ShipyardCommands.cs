using Cocona;
using Microsoft.Extensions.Hosting;
using Wolfe.SpaceTraders.Cli.Formatters;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Cli.Commands;

internal class ShipyardCommands(IExplorationService explorationService, IHostApplicationLifetime host)
{
    public async Task<int> Get([Argument] WaypointId shipyardId)
    {
        var shipyard = await explorationService.GetShipyard(shipyardId, host.ApplicationStopping) ?? throw new Exception($"Shipyard '{shipyardId}' not found.");
        ShipyardFormatter.WriteShipyard(shipyard);

        return ExitCodes.Success;
    }

    public async Task<int> List([Argument] SystemId systemId, [Option] ShipType? selling, [Option] WaypointId? nearestTo)
    {
        var shipyards = explorationService.GetShipyards(systemId, host.ApplicationStopping);

        if (selling != null)
        {
            shipyards = shipyards.Where(s => s.IsSelling(selling.Value));
        }

        Waypoint? relativeWaypoint = null;
        if (nearestTo != null)
        {
            relativeWaypoint = await explorationService.GetWaypoint(nearestTo.Value, host.ApplicationStopping) ?? throw new Exception("Unable to find relative waypoint.");
            shipyards = shipyards.OrderBy(w => w.Location.DistanceTo(relativeWaypoint.Location).Total);
        }

        await foreach (var shipyard in shipyards)
        {
            ShipyardFormatter.WriteShipyard(shipyard, relativeWaypoint?.Location);
            Console.WriteLine();
        }
        return ExitCodes.Success;
    }
}
