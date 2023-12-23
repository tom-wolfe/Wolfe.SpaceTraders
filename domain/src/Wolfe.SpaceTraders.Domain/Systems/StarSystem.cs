using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Domain.Systems;

public class StarSystem
{
    public required SystemSymbol Symbol { get; set; }
    public required SectorSymbol SectorSymbol { get; set; }
    public required SystemType Type { get; set; }
    public required Point Point { get; set; }
    public required List<Waypoint> Waypoints { get; set; } = [];
    public required List<Faction> Factions { get; set; } = [];
}
