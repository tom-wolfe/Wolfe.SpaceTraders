namespace Wolfe.SpaceTraders.Domain.Models;

public class ShipyardShip
{
    public required ShipType Type { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required Credits PurchasePrice { get; set; }
}