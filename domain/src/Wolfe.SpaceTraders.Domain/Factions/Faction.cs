using Wolfe.SpaceTraders.Domain.Exploration;

namespace Wolfe.SpaceTraders.Domain.Factions;

public class Faction
{
    /// <summary>
    /// The ID of the faction.
    /// </summary>
    public FactionId Id { get; init; }

    /// <summary>
    /// Name of the faction.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Description of the faction.
    /// </summary>
    public required string Description { get; init; }

    /// <summary>
    /// The waypoint in which the faction's HQ is located in.
    /// </summary>
    public required WaypointId Headquarters { get; init; }
}