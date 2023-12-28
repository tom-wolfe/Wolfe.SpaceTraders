using Cocona;
using Wolfe.SpaceTraders.Cli.Formatters;
using Wolfe.SpaceTraders.Domain.Systems;
using Wolfe.SpaceTraders.Domain.Waypoints;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands;

internal class WaypointCommands(IExplorationService explorationService)
{
    public async Task<int> Get([Argument] WaypointId waypointId, CancellationToken cancellationToken = default)
    {
        var waypoint = await explorationService.GetWaypoint(waypointId, cancellationToken) ?? throw new Exception($"Waypoint '{waypointId}' not found.");
        WaypointFormatter.WriteWaypoint(waypoint);

        return ExitCodes.Success;
    }

    public async Task<int> List([Argument] SystemId systemId, [Option] WaypointType? type, [Option] WaypointTraitId[]? traits, [Option] WaypointId? nearestTo, CancellationToken cancellationToken = default)
    {
        var waypoints = explorationService.GetWaypoints(systemId, cancellationToken);

        if (type != null)
        {
            waypoints = waypoints.Where(w => w.Type == type);
        }

        if (traits?.Length > 0)
        {
            waypoints = waypoints.Where(w => w.HasAnyTrait(traits));
        }

        Waypoint? relativeWaypoint = null;
        if (nearestTo != null)
        {
            relativeWaypoint = await explorationService.GetWaypoint(nearestTo.Value, cancellationToken) ?? throw new Exception("Unable to find relative waypoint.");
            waypoints = waypoints.OrderBy(w => w.Location.DistanceTo(relativeWaypoint.Location).Total);
        }

        await foreach (var waypoint in waypoints)
        {
            WaypointFormatter.WriteWaypoint(waypoint, relativeWaypoint?.Location);
            Console.WriteLine();
        }
        return ExitCodes.Success;
    }
}
