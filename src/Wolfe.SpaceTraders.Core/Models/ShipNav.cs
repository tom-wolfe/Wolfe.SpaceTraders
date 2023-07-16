namespace Wolfe.SpaceTraders.Core.Models;

public class ShipNav
{
    public required SystemSymbol SystemSymbol { get; set; }
    public required WaypointSymbol WaypointSymbol { get; set; }
    public required ShipNavRoute Route { get; set; }
    public required ShipNavStatus Status { get; set; }
    public required ShipNavFlightMode FlightMode { get; set; }
}