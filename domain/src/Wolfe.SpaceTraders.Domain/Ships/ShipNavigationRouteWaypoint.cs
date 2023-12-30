using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.General;

namespace Wolfe.SpaceTraders.Domain.Ships;

public class ShipNavigationRouteWaypoint
{
    public required WaypointId Id { get; init; }
    public required WaypointType Type { get; init; }
    public required SystemId SystemId { get; init; }
    public required Point Point { get; init; }
}