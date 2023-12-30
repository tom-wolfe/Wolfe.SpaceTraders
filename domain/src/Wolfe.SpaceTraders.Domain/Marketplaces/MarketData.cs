using Wolfe.SpaceTraders.Domain.Exploration;

namespace Wolfe.SpaceTraders.Domain.Marketplaces;

public class MarketData
{
    /// <summary>
    /// The ID of the waypoint where the market is located.
    /// </summary>
    public required WaypointId WaypointId { get; init; }

    /// <summary>
    /// The list of recent transactions at this market.
    /// </summary>
    public required IReadOnlyCollection<MarketTransaction> Transactions { get; init; }

    /// <summary>
    /// The list of goods that are traded at this market.
    /// </summary>
    public required IReadOnlyCollection<MarketTradeGood> TradeGoods { get; init; }

    /// <summary>
    /// The date and time this data was retrieved.
    /// </summary>
    public required DateTimeOffset RetrievedAt { get; init; }

    /// <summary>
    /// The time since this data was retrieved.
    /// </summary>
    public TimeSpan Age => DateTimeOffset.UtcNow - RetrievedAt;
}