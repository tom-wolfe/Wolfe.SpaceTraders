namespace Wolfe.SpaceTraders.Core.Models;

public class ShipCargoInventory
{
    public required InventorySymbol Symbol { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required int Units { get; set; }
}