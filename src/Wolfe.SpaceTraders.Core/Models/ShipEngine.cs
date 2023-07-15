namespace Wolfe.SpaceTraders.Core.Models;

public class ShipEngine
{
    public required ShipEngineSymbol Symbol { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int Condition { get; set; }
    public required int Speed { get; set; }
    public required ShipRequirements Requirements { get; set; }
}