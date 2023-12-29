namespace Wolfe.SpaceTraders.Sdk.Models.Marketplace;

public class SpaceTradersMarketplaceTransaction
{
    /// <summary>
    /// The symbol of the waypoint.
    /// </summary>
    public required string WaypointSymbol { get; set; }

    /// <summary>
    /// The symbol of the ship that made the transaction.
    /// </summary>
    public required string ShipSymbol { get; set; }

    /// <summary>
    /// The symbol of the trade good.
    /// </summary>
    public required string TradeSymbol { get; set; }

    /// <summary>
    /// The type of transaction.
    /// </summary>
    /// <remarks>
    /// Allowed values: PURCHASE, SELL
    /// </remarks>
    public required string Type { get; set; }

    /// <summary>
    /// The number of units of the transaction.
    /// </summary>
    public required int Units { get; set; }

    /// <summary>
    /// The price per unit of the transaction.
    /// </summary>
    public required int PricePerUnit { get; set; }

    /// <summary>
    /// The total price of the transaction.
    /// </summary>
    public required int TotalPrice { get; set; }

    /// <summary>
    /// The timestamp of the transaction.
    /// </summary>
    public required DateTimeOffset Timestamp { get; set; }
}