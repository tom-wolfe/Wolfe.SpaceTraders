using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Domain.Navigation;

public class Navigation
{
    public required WaypointId WaypointId { get; init; }
    public required NavigationRoute Route { get; init; }
    public required NavigationStatus Status { get; init; }
    public required FlightSpeed Speed { get; init; }
}