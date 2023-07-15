namespace Wolfe.SpaceTraders.Models;

public class ShipModule
{
    public required ShipModuleSymbol Symbol { get; set; }
    public int Capacity { get; set; }
    public int Range { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required ShipRequirements Requirements { get; set; }
}