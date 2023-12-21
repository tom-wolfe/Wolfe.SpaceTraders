namespace Wolfe.SpaceTraders.Domain.Models.Ships;

public class ShipCargo
{
    public required int Capacity { get; set; }
    public required int Units { get; set; }
    public decimal PercentRemaining => (decimal)Units / Capacity * 100m;
    public required List<ShipCargoItem> Inventory { get; set; }
}