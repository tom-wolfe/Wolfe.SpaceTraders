namespace Wolfe.SpaceTraders.Core.Models;

public class Navigation
{
    public required SystemSymbol SystemSymbol { get; set; }
    public required WaypointSymbol WaypointSymbol { get; set; }
    public required NavigationRoute Route { get; set; }
    public required NavigationStatus Status { get; set; }
    public required FlightMode FlightMode { get; set; }
}