namespace Wolfe.SpaceTraders.Domain.Waypoints;

public class WaypointTrait
{
    public WaypointTraitSymbol Symbol { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }
}