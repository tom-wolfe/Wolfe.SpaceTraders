using Wolfe.SpaceTraders.Domain.Marketplace;

namespace Wolfe.SpaceTraders.Domain.Ships;

public class ShipCargoItem
{
    public required TradeSymbol Symbol { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required int Units { get; init; }
}