namespace Wolfe.SpaceTraders.Domain.Ships;

public class ShipCargo
{
    public required int Capacity { get; init; }
    public required int Units { get; init; }
    public decimal PercentRemaining => Capacity == 0 ? 0 : (decimal)Units / Capacity * 100m;
    public required List<ShipCargoItem> Inventory { get; init; }
}