using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Domain.Marketplace;

public class Marketplace : Waypoint
{
    public required List<MarketplaceItem> Imports { get; init; } = [];
    public required List<MarketplaceItem> Exports { get; init; } = [];
    public required List<MarketplaceItem> Exchange { get; init; } = [];

    public bool IsBuying(TradeId tradeId) => Imports.Any(e => e.TradeId == tradeId);
    public bool IsSelling(TradeId tradeId) => Exports.Any(e => e.TradeId == tradeId);
}