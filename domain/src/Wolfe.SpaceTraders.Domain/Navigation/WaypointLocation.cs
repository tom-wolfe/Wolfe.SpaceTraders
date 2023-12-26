using Wolfe.SpaceTraders.Domain.Systems;
using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Domain.Navigation;

public class WaypointLocation
{
    public required WaypointSymbol Symbol { get; init; }
    public required WaypointType Type { get; init; }
    public required SystemSymbol SystemSymbol { get; init; }
    public required Point Point { get; init; }
}