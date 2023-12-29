using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Domain.Marketplaces;

public class MarketTransaction
{
    /// <summary>
    /// The ID of the ship that made the transaction.
    /// </summary>
    public required ShipId ShipId { get; init; }

    /// <summary>
    /// The ID of the trade good.
    /// </summary>
    public required ItemId ItemId { get; init; }

    /// <summary>
    /// The type of transaction.
    /// </summary>
    public required TransactionType Type { get; init; }

    /// <summary>
    /// The number of units of the transaction.
    /// </summary>
    public required int Quantity { get; init; }

    /// <summary>
    /// The price per unit of the transaction.
    /// </summary>
    public required Credits PricePerUnit { get; init; }

    /// <summary>
    /// The total price of the transaction.
    /// </summary>
    public Credits TotalPrice => PricePerUnit * Quantity;

    /// <summary>
    /// The timestamp of the transaction.
    /// </summary>
    public required DateTimeOffset Timestamp { get; init; }
}