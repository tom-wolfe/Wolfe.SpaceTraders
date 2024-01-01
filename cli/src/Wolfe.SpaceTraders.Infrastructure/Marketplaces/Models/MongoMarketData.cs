using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Wolfe.SpaceTraders.Infrastructure.Marketplaces.Models;

internal class MongoMarketData
{
    [BsonId]
    public required string WaypointId { get; init; }
    public required IReadOnlyCollection<MongoMarketTransaction> Transactions { get; init; }
    public required IReadOnlyCollection<MongoMarketTradeGood> TradeGoods { get; init; }

    [BsonRepresentation(BsonType.String)]
    public required DateTimeOffset RetrievedAt { get; init; }
}

internal class MongoMarketTradeGood
{
    public required string ItemId { get; init; }
    public required string Type { get; init; }
    public required int Volume { get; init; }
    public required string Supply { get; init; }
    public required string? Activity { get; init; }
    public required long PurchasePrice { get; init; }
    public required long SellPrice { get; init; }
}

internal class MongoMarketTransaction
{
    public required string ShipId { get; init; }
    public required string ItemId { get; init; }
    public required string Type { get; init; }
    public required int Quantity { get; init; }
    public required long PricePerUnit { get; init; }
    public required DateTimeOffset Timestamp { get; init; }
}