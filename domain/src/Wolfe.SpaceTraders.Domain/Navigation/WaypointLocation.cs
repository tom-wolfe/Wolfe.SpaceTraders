using Wolfe.SpaceTraders.Domain.Systems;
using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Domain.Navigation;

public class WaypointLocation
{
    public required WaypointSymbol Symbol { get; set; }
    public required WaypointType Type { get; set; }
    public required SystemSymbol SystemSymbol { get; set; }
    public required Point Point { get; set; }
}