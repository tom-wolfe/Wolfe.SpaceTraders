namespace Wolfe.SpaceTraders.Infrastructure.Data.Models;

internal class DataMarketData
{
    public required string WaypointId { get; init; }
    public required IReadOnlyCollection<DataMarketTransaction> Transactions { get; init; }
    public required IReadOnlyCollection<DataMarketTradeGood> TradeGoods { get; init; }
    public required DateTimeOffset RetrievedAt { get; init; }
}

internal class DataMarketTradeGood
{
    public required string ItemId { get; init; }
    public required string Type { get; init; }
    public required int Volume { get; init; }
    public required string Supply { get; init; }
    public required string Activity { get; init; }
    public required long PurchasePrice { get; init; }
    public required long SellPrice { get; init; }
}

internal class DataMarketTransaction
{
    public required string ShipId { get; init; }
    public required string ItemId { get; init; }
    public required string Type { get; init; }
    public required int Quantity { get; init; }
    public required long PricePerUnit { get; init; }
    public required DateTimeOffset Timestamp { get; init; }
}