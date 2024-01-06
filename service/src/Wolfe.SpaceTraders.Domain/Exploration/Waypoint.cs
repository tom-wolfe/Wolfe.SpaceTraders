using Wolfe.SpaceTraders.Domain.General;

namespace Wolfe.SpaceTraders.Domain.Exploration;

/// <summary>
/// Represents a point of interest in the universe.
/// </summary>
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

    /// <inheritdoc />
    public override bool Equals(object? obj) => Equals(obj as Waypoint);

    /// <inheritdoc />
    public override int GetHashCode() => Id.GetHashCode();

    /// <summary>
    /// Checks whether two waypoints are equal.
    /// </summary>
    /// <param name="other">The waypoint to check.</param>
    /// <returns>True if the waypoints have the same ID. Otherwise, false.</returns>
    protected bool Equals(Waypoint? other) => Id.Equals(other?.Id);

    /// <summary>
    /// Checks whether two waypoints are equal.
    /// </summary>
    /// <param name="left">The first waypoint to check.</param>
    /// <param name="right">The second waypoint to check.</param>
    /// <returns>True if the waypoints have the same ID. Otherwise, false.</returns>
    public static bool operator ==(Waypoint? left, Waypoint? right) => Equals(left, right);

    /// <summary>
    /// Checks whether two waypoints are different.
    /// </summary>
    /// <param name="left">The first waypoint to check.</param>
    /// <param name="right">The second waypoint to check.</param>
    /// <returns>True if the waypoints have different IDs. Otherwise, false.</returns>
    public static bool operator !=(Waypoint? left, Waypoint? right) => !(left == right);
}