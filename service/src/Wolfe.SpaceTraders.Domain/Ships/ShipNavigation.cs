using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.General;

namespace Wolfe.SpaceTraders.Domain.Ships;

internal class ShipNavigation : IShipNavigation
{
    public ShipNavigation(IShipNavigation navigation)
    {
        WaypointId = navigation.WaypointId;
        Location = navigation.Location;
        Status = navigation.Status;
        Speed = navigation.Speed;
        Destination = navigation.Destination;
    }

    public WaypointId WaypointId { get; set; }
    public Point Location { get; set; }
    public ShipNavigationStatus Status { get; set; }
    public ShipSpeed Speed { get; set; }
    public ShipNavigationDestination? Destination { get; set; }
}