namespace Wolfe.SpaceTraders.Models;

public class ShipReactor
{
    public required ShipReactorSymbol Symbol { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int Condition { get; set; }
    public int PowerOutput { get; set; }
    public required ShipRequirements Requirements { get; set; }
}