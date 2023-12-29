using Wolfe.SpaceTraders.Domain.General;

namespace Wolfe.SpaceTraders.Domain.Marketplaces;

public class MarketTradeGood
{
    /// <summary>
    /// The ID of the good being traded.
    /// </summary>
    public required ItemId ItemId { get; init; }

    /// <summary>
    /// The type of trade good (export, import, or exchange).
    /// </summary>
    public required MarketTradeType Type { get; init; }

    /// <summary>
    /// This is the maximum number of units that can be purchased or sold at this market in a single trade for this good.
    /// Trade volume also gives an indication of price volatility.
    /// A market with a low trade volume will have large price swings, while high trade volume will be more resilient to price changes.
    /// </summary>
    public required int Volume { get; init; }

    /// <summary>
    /// The supply level of a trade good.
    /// </summary>
    public required MarketTradeSupply Supply { get; init; }

    /// <summary>
    /// The activity level of a trade good. If the good is an import, this represents how strong consumption is.
    /// If the good is an export, this represents how strong the production is for the good. When activity is strong,
    /// consumption or production is near maximum capacity. When activity is weak, consumption or production is near minimum capacity.
    /// </summary>
    public required MarketTradeActivity Activity { get; init; }

    /// <summary>
    /// The price at which this good can be purchased from the market.
    /// </summary>
    public required Credits PurchasePrice { get; init; }

    /// <summary>
    /// The price at which this good can be sold to the market.
    /// </summary>
    public required Credits SellPrice { get; init; }
}