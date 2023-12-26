using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Domain.Systems;

public class StarSystem
{
    public required SystemSymbol Symbol { get; init; }
    public required SectorSymbol SectorSymbol { get; init; }
    public required SystemType Type { get; init; }
    public required Point Location { get; init; }
    public required List<Waypoint> Waypoints { get; init; } = [];
    public required List<Faction> Factions { get; init; } = [];
}
