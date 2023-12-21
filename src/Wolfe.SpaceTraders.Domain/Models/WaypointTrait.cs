namespace Wolfe.SpaceTraders.Domain.Models;

public class WaypointTrait
{
    public WaypointTraitSymbol Symbol { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }
}