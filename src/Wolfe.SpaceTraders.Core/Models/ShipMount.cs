namespace Wolfe.SpaceTraders.Core.Models;

public class ShipMount
{
    public required ShipMountSymbol Symbol { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int Strength { get; set; }
    public List<DepositSymbol>? Deposits { get; set; }
    public required ShipRequirements Requirements { get; set; }
}