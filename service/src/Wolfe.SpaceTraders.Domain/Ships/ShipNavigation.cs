using Wolfe.SpaceTraders.Domain.Exploration;

namespace Wolfe.SpaceTraders.Domain.Ships;

public class ShipNavigation
{
    public required WaypointId WaypointId { get; init; }
    public required ShipNavigationRoute Route { get; init; }
    public required ShipNavigationStatus Status { get; init; }
    public required ShipSpeed Speed { get; init; }
}