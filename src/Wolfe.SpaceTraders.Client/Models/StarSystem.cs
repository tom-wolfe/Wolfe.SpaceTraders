namespace Wolfe.SpaceTraders.Models;

public class StarSystem
{
    public required SystemSymbol Symbol { get; set; }
    public required SectorSymbol SectorSymbol { get; set; }
    public required SystemType Type { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public List<ShallowWaypoint> Waypoints { get; } = new();
    public List<ShallowFaction> Factions { get; } = new();
}
