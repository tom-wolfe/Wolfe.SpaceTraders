namespace Wolfe.SpaceTraders.Models;

public class StarSystem
{
    public required SystemSymbol Symbol { get; set; }
    public required SectorSymbol SectorSymbol { get; set; }
    public required SystemType Type { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public required List<ShallowWaypoint> Waypoints { get; set; } = new();
    public required List<ShallowFaction> Factions { get; set; } = new();
}
