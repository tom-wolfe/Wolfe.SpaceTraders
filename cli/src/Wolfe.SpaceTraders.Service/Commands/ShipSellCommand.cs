using Wolfe.SpaceTraders.Domain.Marketplace;

namespace Wolfe.SpaceTraders.Service.Commands;

public class ShipSellCommand
{
    public required TradeSymbol ItemId { get; set; }
    public required int Quantity { get; set; }
}