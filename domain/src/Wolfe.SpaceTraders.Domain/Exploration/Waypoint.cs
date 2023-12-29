using Wolfe.SpaceTraders.Domain.Navigation;

namespace Wolfe.SpaceTraders.Domain.Exploration;

public class Waypoint
{
    public required WaypointId Id { get; init; }
    public required WaypointType Type { get; init; }
    public SystemId SystemId => Id.System;
    public required Point Location { get; init; }
    public required IReadOnlyCollection<WaypointTrait> Traits { get; init; } = [];

    public bool IsMarketplace => HasTrait(WaypointTraitId.Marketplace);
    public bool IsShipyard => HasTrait(WaypointTraitId.Shipyard);

    public bool HasTrait(WaypointTraitId trait) =>
        Traits.Any(x => x.Id == trait);

    public bool HasAnyTrait(IEnumerable<WaypointTraitId> traits) =>
        Traits.Select(t => t.Id).Intersect(traits).Any();
}