using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Domain.Marketplace;

public class Transaction
{
    public required WaypointSymbol WaypointSymbol { get; init; }
    public required ShipSymbol ShipSymbol { get; init; }
    public required TradeSymbol TradeSymbol { get; init; }
    public required TransactionType Type { get; init; }
    public required int Units { get; init; }
    public required Credits PricePerUnit { get; init; }
    public required Credits TotalPrice { get; init; }
    public required DateTimeOffset Timestamp { get; init; }
}