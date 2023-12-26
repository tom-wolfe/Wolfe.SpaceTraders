using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Domain.Systems;

namespace Wolfe.SpaceTraders.Domain.Waypoints;

public class Waypoint
{
    public required WaypointSymbol Symbol { get; init; }
    public required WaypointType Type { get; init; }
    public required SystemSymbol SystemSymbol { get; init; }
    public required Point Location { get; init; }
    public required IReadOnlyCollection<WaypointTrait> Traits { get; init; } = [];

    public bool HasTrait(WaypointTraitSymbol trait) =>
        Traits.Any(x => x.Symbol == trait);

    public bool HasAnyTrait(IEnumerable<WaypointTraitSymbol> traits) =>
        Traits.Select(t => t.Symbol).Intersect(traits).Any();
}