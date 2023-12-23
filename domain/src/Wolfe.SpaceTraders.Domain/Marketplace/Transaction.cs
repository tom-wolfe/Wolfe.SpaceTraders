using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Domain.Marketplace;

public class Transaction
{
    public required WaypointSymbol WaypointSymbol { get; set; }
    public required ShipSymbol ShipSymbol { get; set; }
    public required TradeSymbol TradeSymbol { get; set; }
    public required TransactionType Type { get; set; }
    public required int Units { get; set; }
    public required Credits PricePerUnit { get; set; }
    public required Credits TotalPrice { get; set; }
    public required DateTimeOffset Timestamp { get; set; }
}