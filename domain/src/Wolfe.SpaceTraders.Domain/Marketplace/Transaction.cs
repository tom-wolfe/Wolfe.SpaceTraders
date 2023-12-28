using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Domain.Marketplace;

public class Transaction
{
    public required WaypointId WaypointId { get; init; }
    public required ShipId ShipId { get; init; }
    public required ItemId ItemId { get; init; }
    public required TransactionType Type { get; init; }
    public required int Quantity { get; init; }
    public required Credits PricePerUnit { get; init; }
    public Credits TotalPrice => PricePerUnit * Quantity;
    public required DateTimeOffset Timestamp { get; init; }
}