namespace Wolfe.SpaceTraders.Domain.Exploration;

public class WaypointTrait
{
    /// <summary>
    /// The unique identifier of the trait.
    /// </summary>
    public WaypointTraitId Id { get; init; }

    /// <summary>
    /// The name of the trait.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// A description of the trait.
    /// </summary>
    public required string Description { get; init; }
}