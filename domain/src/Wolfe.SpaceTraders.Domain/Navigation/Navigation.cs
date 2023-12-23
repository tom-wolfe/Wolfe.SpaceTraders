using Wolfe.SpaceTraders.Domain.Systems;
using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Domain.Navigation;

public class Navigation
{
    public required SystemSymbol SystemSymbol { get; set; }
    public required WaypointSymbol WaypointSymbol { get; set; }
    public required NavigationRoute Route { get; set; }
    public required NavigationStatus Status { get; set; }
    public required FlightSpeed Speed { get; set; }
}