namespace Wolfe.SpaceTraders.Core.Models;

public class ShipNavLocation
{
    public required WaypointSymbol Symbol { get; set; }
    public required WaypointType Type { get; set; }
    public required SystemSymbol SystemSymbol { get; set; }
    public required int X { get; set; }
    public required int Y { get; set; }
}