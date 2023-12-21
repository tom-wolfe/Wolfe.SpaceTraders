namespace Wolfe.SpaceTraders.Domain.Models;

public class StarSystem
{
    public required SystemSymbol Symbol { get; set; }
    public required SectorSymbol SectorSymbol { get; set; }
    public required SystemType Type { get; set; }
    public required Location Location { get; set; }
    public required List<Waypoint> Waypoints { get; set; } = new();
    public required List<Faction> Factions { get; set; } = new();
}
