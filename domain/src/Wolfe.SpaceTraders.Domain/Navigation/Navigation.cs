using Wolfe.SpaceTraders.Domain.Exploration;

namespace Wolfe.SpaceTraders.Domain.Navigation;

public class Navigation
{
    public required WaypointId WaypointId { get; init; }
    public required NavigationRoute Route { get; init; }
    public required NavigationStatus Status { get; init; }
    public required ShipSpeed Speed { get; init; }
}