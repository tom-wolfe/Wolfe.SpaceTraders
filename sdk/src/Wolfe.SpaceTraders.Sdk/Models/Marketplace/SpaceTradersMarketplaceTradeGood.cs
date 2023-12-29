namespace Wolfe.SpaceTraders.Sdk.Models.Marketplace;

public class SpaceTradersMarketplaceTradeGood
{
    /// <summary>
    /// The good's symbol.
    /// </summary>
    public required string Symbol { get; set; }

    /// <summary>
    /// The type of trade good (export, import, or exchange).
    /// </summary>
    /// <remarks>
    /// Allowed values: IMPORT, EXPORT, EXCHANGE
    /// </remarks>
    public required string Type { get; set; }

    /// <summary>
    /// This is the maximum number of units that can be purchased or sold at this market in a single trade for this good.
    /// Trade volume also gives an indication of price volatility.
    /// A market with a low trade volume will have large price swings, while high trade volume will be more resilient to price changes.
    /// </summary>
    public required int TradeVolume { get; set; }

    /// <summary>
    /// The supply level of a trade good.
    /// </summary>
    /// <remarks>
    /// Allowed values: SCARCE, LIMITED, MODERATE, HIGH, ABUNDANT
    /// </remarks>
    public required string Supply { get; set; }

    /// <summary>
    /// The activity level of a trade good. If the good is an import, this represents how strong consumption is.
    /// If the good is an export, this represents how strong the production is for the good.
    /// When activity is strong, consumption or production is near maximum capacity.
    /// When activity is weak, consumption or production is near minimum capacity.
    /// </summary>
    /// <remarks>
    /// Allowed values: WEAK, GROWING, STRONG, RESTRICTED
    /// </remarks>
    public required string Activity { get; set; }

    /// <summary>
    /// The price at which this good can be purchased from the market.
    /// </summary>
    public required int PurchaseSPrice { get; set; }

    /// <summary>
    /// The price at which this good can be sold to the market.
    /// </summary>
    public required int SellPrice { get; set; }
}