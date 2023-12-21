namespace Wolfe.SpaceTraders.Domain.Models.Waypoints;

public class WaypointTrait
{
    public WaypointTraitSymbol Symbol { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }
}