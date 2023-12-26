using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Domain.Marketplace;

public class Marketplace : Waypoint
{
    public required List<MarketplaceItem> Imports { get; init; } = [];
    public required List<MarketplaceItem> Exports { get; init; } = [];
    public required List<MarketplaceItem> Exchange { get; init; } = [];

    public bool IsBuying(TradeSymbol symbol) => Imports.Any(e => e.Symbol == symbol);
    public bool IsSelling(TradeSymbol symbol) => Exports.Any(e => e.Symbol == symbol);
}