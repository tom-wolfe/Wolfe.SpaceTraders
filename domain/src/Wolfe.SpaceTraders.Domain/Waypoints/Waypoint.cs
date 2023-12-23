using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Domain.Systems;

namespace Wolfe.SpaceTraders.Domain.Waypoints;

public class Waypoint
{
    public required WaypointSymbol Symbol { get; set; }
    public required WaypointType Type { get; set; }
    public required SystemSymbol SystemSymbol { get; set; }
    public required Point Point { get; set; }
    public required List<WaypointTrait> Traits { get; set; } = [];
}