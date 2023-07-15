namespace Wolfe.SpaceTraders.Core.Models;

public class ShallowStarSystem
{
    public required SystemSymbol Symbol { get; set; }
    public required SystemType Type { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public required List<ShallowWaypoint> Waypoints { get; set; }
    public required List<ShallowFaction> Factions { get; set; }
}
