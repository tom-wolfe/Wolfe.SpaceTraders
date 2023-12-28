using Cocona;
using Wolfe.SpaceTraders.Cli.Formatters;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Domain.Systems;
using Wolfe.SpaceTraders.Domain.Waypoints;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands;

internal class ShipyardCommands(IExplorationService explorationService)
{
    public async Task<int> Get([Argument] WaypointId shipyardId, CancellationToken cancellationToken = default)
    {
        var shipyard = await explorationService.GetShipyard(shipyardId, cancellationToken) ?? throw new Exception($"Shipyard '{shipyardId}' not found.");
        ShipyardFormatter.WriteShipyard(shipyard);

        return ExitCodes.Success;
    }

    public async Task<int> List([Argument] SystemId systemId, [Option] ShipType? selling, [Option] WaypointId? nearestTo, CancellationToken cancellationToken = default)
    {
        var shipyards = explorationService.GetShipyards(systemId, cancellationToken);

        if (selling != null)
        {
            shipyards = shipyards.Where(s => s.IsSelling(selling.Value));
        }

        Waypoint? relativeWaypoint = null;
        if (nearestTo != null)
        {
            relativeWaypoint = await explorationService.GetWaypoint(nearestTo.Value, cancellationToken) ?? throw new Exception("Unable to find relative waypoint.");
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
