using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Domain.Systems;
using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Domain.Ships.Results;

public class SetShipSpeedResult
{
    public required SystemId SystemId { get; set; }
    public required WaypointId WaypointId { get; set; }
    public required NavigationStatus Status { get; set; }
    public required FlightSpeed Speed { get; set; }
    public required NavigationRoute Route { get; set; }
}