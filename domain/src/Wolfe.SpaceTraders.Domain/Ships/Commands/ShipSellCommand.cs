using Wolfe.SpaceTraders.Domain.Marketplace;

namespace Wolfe.SpaceTraders.Domain.Ships.Commands;

public class ShipSellCommand
{
    public required TradeId ItemId { get; set; }
    public required int Quantity { get; set; }
}