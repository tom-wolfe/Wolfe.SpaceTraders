namespace Wolfe.SpaceTraders.Sdk.Models.Marketplace;

public class SpaceTradersMarketplace
{
    /// <summary>
    /// The symbol of the market. The symbol is the same as the waypoint where the market is located.
    /// </summary>
    public required string Symbol { get; set; }

    /// <summary>
    /// The list of goods that are sought as imports in this market.
    /// </summary>
    public required IReadOnlyCollection<SpaceTradersMarketplaceItem> Imports { get; set; }

    /// <summary>
    /// The list of goods that are exported from this market.
    /// </summary>
    public required IReadOnlyCollection<SpaceTradersMarketplaceItem> Exports { get; set; }

    /// <summary>
    /// The list of goods that are bought and sold between agents at this market.
    /// </summary>
    public required IReadOnlyCollection<SpaceTradersMarketplaceItem> Exchange { get; set; }

    /// <summary>
    /// The list of recent transactions at this market. Visible only when a ship is present at the market.
    /// </summary>
    public required IReadOnlyCollection<SpaceTradersMarketplaceTransaction>? Transactions { get; set; }

    /// <summary>
    /// The list of goods that are traded at this market. Visible only when a ship is present at the market.
    /// </summary>
    public required IReadOnlyCollection<SpaceTradersMarketplaceTradeGood>? TradeGoods { get; set; }
}