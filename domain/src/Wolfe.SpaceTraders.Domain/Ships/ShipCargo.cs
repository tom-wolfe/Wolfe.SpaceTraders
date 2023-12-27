using Wolfe.SpaceTraders.Domain.Marketplace;

namespace Wolfe.SpaceTraders.Domain.Ships;

public class ShipCargo(IShipClient client)
{
    public required int Capacity { get; init; }
    public required int Quantity { get; init; }
    public decimal PercentRemaining => Capacity == 0 ? 0 : (decimal)Quantity / Capacity * 100m;
    public required List<ShipCargoItem> Inventory { get; init; }

    public async Task Jettison(TradeId tradeId, int quantity)
    {
        // TODO: Implement.
    }
}