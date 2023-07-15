namespace Wolfe.SpaceTraders.Core.Models;

public class ShallowWaypoint
{
    public required WaypointSymbol Symbol { get; set; }
    public required WaypointType Type { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}