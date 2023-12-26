using Wolfe.SpaceTraders.Domain.Systems;
using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Domain.Navigation;

public class Navigation
{
    public required SystemSymbol SystemSymbol { get; init; }
    public required WaypointSymbol WaypointSymbol { get; init; }
    public required NavigationRoute Route { get; init; }
    public required NavigationStatus Status { get; init; }
    public required FlightSpeed Speed { get; init; }
}