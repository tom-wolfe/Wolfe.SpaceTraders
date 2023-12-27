using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Domain.Systems;

namespace Wolfe.SpaceTraders.Domain.Waypoints;

public class Waypoint
{
    public required WaypointId Id { get; init; }
    public required WaypointType Type { get; init; }
    public required SystemId SystemId { get; init; }
    public required Point Location { get; init; }
    public required IReadOnlyCollection<WaypointTrait> Traits { get; init; } = [];

    public bool HasTrait(WaypointTraitId trait) =>
        Traits.Any(x => x.Id == trait);

    public bool HasAnyTrait(IEnumerable<WaypointTraitId> traits) =>
        Traits.Select(t => t.Id).Intersect(traits).Any();
}