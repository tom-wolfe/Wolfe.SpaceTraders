using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Domain.Systems;

namespace Wolfe.SpaceTraders.Domain.Waypoints;

public class Waypoint
{
    public required WaypointSymbol Symbol { get; init; }
    public required WaypointType Type { get; init; }
    public required SystemSymbol SystemSymbol { get; init; }
    public required Point Point { get; init; }
    public required List<WaypointTrait> Traits { get; init; } = [];
}