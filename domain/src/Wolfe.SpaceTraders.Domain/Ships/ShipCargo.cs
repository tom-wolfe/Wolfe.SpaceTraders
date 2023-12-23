namespace Wolfe.SpaceTraders.Domain.Ships;

public class ShipCargo
{
    public required int Capacity { get; set; }
    public required int Units { get; set; }
    public decimal PercentRemaining => Capacity == 0 ? 0 : (decimal)Units / Capacity * 100m;
    public required List<ShipCargoItem> Inventory { get; set; }
}