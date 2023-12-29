using Wolfe.SpaceTraders.Domain.Navigation;

namespace Wolfe.SpaceTraders.Domain.Exploration;

public class Waypoint
{
    /// <summary>
    /// The ID of the waypoint.
    /// </summary>
    public required WaypointId Id { get; init; }

    /// <summary>
    /// The type of waypoint.
    /// </summary>
    public required WaypointType Type { get; init; }

    /// <summary>
    /// The system in which the waypoint is located.
    /// </summary>
    public SystemId SystemId => Id.SystemId;

    /// <summary>
    /// Relative position of the waypoint in the system. This is not an absolute position in the universe.
    /// </summary>
    public required Point Location { get; init; }

    /// <summary>
    /// The traits of the waypoint.
    /// </summary>
    public required IReadOnlyCollection<WaypointTrait> Traits { get; init; } = [];

    /// <summary>
    /// True if the waypoint has a marketplace.
    /// </summary>
    public bool HasMarketplace => HasTrait(WaypointTraitId.Marketplace);

    /// <summary>
    /// True if the waypoint has a shipyard.
    /// </summary>
    public bool HasShipyard => HasTrait(WaypointTraitId.Shipyard);

    /// <summary>
    /// Determines if the waypoint has a specific trait.
    /// </summary>
    /// <param name="trait">The trait to look for.</param>
    /// <returns>True if the waypoint has the trait; otherwise, false.</returns>
    public bool HasTrait(WaypointTraitId trait) => Traits.Any(x => x.Id == trait);

    /// <summary>
    /// Determines if the waypoint has any of the specified traits.
    /// </summary>
    /// <param name="traits">The traits to look for.</param>
    /// <returns>True if the waypoint has at least one of the traits; otherwise, false.</returns>
    public bool HasAnyTrait(IEnumerable<WaypointTraitId> traits) => Traits.Select(t => t.Id).Intersect(traits).Any();
}