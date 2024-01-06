using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.General;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Infrastructure.Exploration.Models;

namespace Wolfe.SpaceTraders.Infrastructure.Marketplaces.Models;

internal static class Mapping
{
    public static MongoMarketplace ToMongo(this Marketplace marketplace) => new()
    {
        Id = marketplace.Id.Value,
        SystemId = marketplace.Id.SystemId.Value,
        Type = marketplace.Type.Value,
        Location = marketplace.Location.ToMongo(),
        Traits = marketplace.Traits.Select(t => t.ToMongo()).ToList(),
        Imports = marketplace.Imports.Select(i => i.ToMongo()).ToList(),
        Exports = marketplace.Exports.Select(i => i.ToMongo()).ToList(),
        Exchange = marketplace.Exchange.Select(i => i.ToMongo()).ToList(),
    };

    public static Marketplace ToDomain(this MongoMarketplace marketplace) => new()
    {
        Id = new WaypointId(marketplace.Id),
        Type = new WaypointType(marketplace.Type),
        Location = marketplace.Location.ToDomain(),
        Traits = marketplace.Traits.Select(t => t.ToDomain()).ToList(),
        Imports = marketplace.Imports.Select(i => i.ToDomain()).ToList(),
        Exports = marketplace.Exports.Select(i => i.ToDomain()).ToList(),
        Exchange = marketplace.Exchange.Select(i => i.ToDomain()).ToList(),
    };

    private static MongoMarketplaceItem ToMongo(this MarketplaceItem item) => new(item.ItemId.Value, item.Name, item.Description);

    private static MarketplaceItem ToDomain(this MongoMarketplaceItem item) => new()
    {
        ItemId = new ItemId(item.Id),
        Name = item.Name,
        Description = item.Description,
    };

    public static MongoMarketData ToMongo(this MarketData marketData) => new()
    {
        WaypointId = marketData.WaypointId.Value,
        SystemId = marketData.WaypointId.SystemId.Value,
        RetrievedAt = marketData.RetrievedAt,
        TradeGoods = marketData.TradeGoods.Select(t => t.ToMongo()).ToList(),
        Transactions = marketData.Transactions.Select(t => t.ToMongo()).ToList(),
    };
    public static MarketData ToDomain(this MongoMarketData marketData) => new()
    {
        WaypointId = new WaypointId(marketData.WaypointId),
        RetrievedAt = marketData.RetrievedAt,
        TradeGoods = marketData.TradeGoods.Select(t => t.ToDomain()).ToList(),
        Transactions = marketData.Transactions.Select(t => t.ToDomain()).ToList(),
    };

    public static MarketTradeGood ToDomain(this MongoMarketTradeGood tradeGood) => new()
    {
        ItemId = new ItemId(tradeGood.ItemId),
        Type = new MarketTradeType(tradeGood.Type),
        Activity = tradeGood.Activity == null ? null : new MarketTradeActivity(tradeGood.Activity),
        Supply = new MarketTradeSupply(tradeGood.Supply),
        Volume = tradeGood.Volume,
        PurchasePrice = new Credits(tradeGood.PurchasePrice),
        SellPrice = new Credits(tradeGood.SellPrice),
    };

    public static MongoMarketTradeGood ToMongo(this MarketTradeGood marketData) => new()
    {
        ItemId = marketData.ItemId.Value,
        Type = marketData.Type.Value,
        Activity = marketData.Activity?.Value,
        Supply = marketData.Supply.Value,
        Volume = marketData.Volume,
        PurchasePrice = marketData.PurchasePrice.Value,
        SellPrice = marketData.SellPrice.Value,
    };

    public static MarketTransaction ToDomain(this MongoMarketTransaction transaction) => new()
    {
        ShipId = new ShipId(transaction.ShipId),
        ItemId = new ItemId(transaction.ItemId),
        Type = new TransactionType(transaction.Type),
        Quantity = transaction.Quantity,
        PricePerUnit = new Credits(transaction.PricePerUnit),
        Timestamp = transaction.Timestamp,
    };

    public static MongoMarketTransaction ToMongo(this MarketTransaction transaction) => new()
    {
        ShipId = transaction.ShipId.Value,
        ItemId = transaction.ItemId.Value,
        Type = transaction.Type.Value,
        Quantity = transaction.Quantity,
        PricePerUnit = transaction.PricePerUnit.Value,
        Timestamp = transaction.Timestamp,
    };
}
