using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Domain.Systems;
using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Service.Results;

public class SetShipSpeedResult
{
    public required SystemSymbol SystemSymbol { get; set; }
    public required WaypointSymbol WaypointSymbol { get; set; }
    public required NavigationStatus Status { get; set; }
    public required FlightSpeed Speed { get; set; }
    public required NavigationRoute Route { get; set; }
}