namespace Wolfe.SpaceTraders.Core.Models;

public class ShipCargo
{
    public required int Capacity { get; set; }
    public required int Units { get; set; }
    public required List<ShipCargoInventory> Inventory { get; set; } = new();
}