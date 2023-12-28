namespace Wolfe.SpaceTraders.Domain.Ships;

public class ShipCargo
{
    public required int Capacity { get; init; }
    public required int Quantity { get; init; }
    public decimal PercentRemaining => Capacity == 0 ? 0 : (decimal)Quantity / Capacity * 100m;
    public required List<ShipCargoItem> Items { get; init; }
}