namespace Wolfe.SpaceTraders.Domain.Waypoints;

public class WaypointTrait
{
    public WaypointTraitId Id { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }
}