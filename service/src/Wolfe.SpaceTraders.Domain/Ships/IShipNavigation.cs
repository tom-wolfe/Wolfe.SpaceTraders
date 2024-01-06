using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.General;

namespace Wolfe.SpaceTraders.Domain.Ships;

public interface IShipNavigation
{
    WaypointId WaypointId { get; }
    Point Location { get; }
    ShipNavigationStatus Status { get; }
    ShipSpeed Speed { get; }
    ShipNavigationDestination? Destination { get; }
}