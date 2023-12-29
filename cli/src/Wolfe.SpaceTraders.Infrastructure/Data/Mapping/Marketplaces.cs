using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.General;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Infrastructure.Data.Models;

namespace Wolfe.SpaceTraders.Infrastructure.Data.Mapping;

internal static class Marketplaces
{
    public static DataMarketplace ToData(this Marketplace marketplace) => new()
    {
        Id = marketplace.Id.Value,
        Type = marketplace.Type.Value,
        Location = marketplace.Location.ToData(),
        Traits = marketplace.Traits.Select(t => t.ToData()).ToList(),
        Imports = marketplace.Imports.Select(i => i.ToData()).ToList(),
        Exports = marketplace.Exports.Select(i => i.ToData()).ToList(),
        Exchange = marketplace.Exchange.Select(i => i.ToData()).ToList(),
    };

    public static Marketplace ToDomain(this DataMarketplace marketplace) => new()
    {
        Id = new WaypointId(marketplace.Id),
        Type = new WaypointType(marketplace.Type),
        Location = marketplace.Location.ToDomain(),
        Traits = marketplace.Traits.Select(t => t.ToDomain()).ToList(),
        Imports = marketplace.Imports.Select(i => i.ToDomain()).ToList(),
        Exports = marketplace.Exports.Select(i => i.ToDomain()).ToList(),
        Exchange = marketplace.Exchange.Select(i => i.ToDomain()).ToList(),
    };

    private static DataMarketplaceItem ToData(this MarketplaceItem item) => new()
    {
        Id = item.ItemId.Value,
        Name = item.Name,
        Description = item.Description,
    };

    private static MarketplaceItem ToDomain(this DataMarketplaceItem item) => new()
    {
        ItemId = new ItemId(item.Id),
        Name = item.Name,
        Description = item.Description,
    };

    public static DataMarketData ToData(this MarketData marketData) => new()
    {
        WaypointId = marketData.WaypointId.Value,
        RetrievedAt = marketData.RetrievedAt,
        TradeGoods = marketData.TradeGoods.Select(t => t.ToData()).ToList(),
        Transactions = marketData.Transactions.Select(t => t.ToData()).ToList(),
    };

    public static MarketData ToDomain(this DataMarketData marketData) => new()
    {
        WaypointId = new WaypointId(marketData.WaypointId),
        RetrievedAt = marketData.RetrievedAt,
        TradeGoods = marketData.TradeGoods.Select(t => t.ToDomain()).ToList(),
        Transactions = marketData.Transactions.Select(t => t.ToDomain()).ToList(),
    };

    public static MarketTradeGood ToDomain(this DataMarketTradeGood tradeGood) => new()
    {
        ItemId = new ItemId(tradeGood.ItemId),
        Type = new MarketTradeType(tradeGood.Type),
        Activity = new MarketTradeActivity(tradeGood.Activity),
        Supply = new MarketTradeSupply(tradeGood.Supply),
        Volume = tradeGood.Volume,
        PurchasePrice = new Credits(tradeGood.PurchasePrice),
        SellPrice = new Credits(tradeGood.SellPrice),
    };

    public static DataMarketTradeGood ToData(this MarketTradeGood marketData) => new()
    {
        ItemId = marketData.ItemId.Value,
        Type = marketData.Type.Value,
        Activity = marketData.Activity.Value,
        Supply = marketData.Supply.Value,
        Volume = marketData.Volume,
        PurchasePrice = marketData.PurchasePrice.Value,
        SellPrice = marketData.SellPrice.Value,
    };

    public static MarketTransaction ToDomain(this DataMarketTransaction transaction) => new()
    {
        ShipId = new ShipId(transaction.ShipId),
        ItemId = new ItemId(transaction.ItemId),
        Type = new TransactionType(transaction.Type),
        Quantity = transaction.Quantity,
        PricePerUnit = new Credits(transaction.PricePerUnit),
        Timestamp = transaction.Timestamp,
    };

    public static DataMarketTransaction ToData(this MarketTransaction transaction) => new()
    {
        ShipId = transaction.ShipId.Value,
        ItemId = transaction.ItemId.Value,
        Type = transaction.Type.Value,
        Quantity = transaction.Quantity,
        PricePerUnit = transaction.PricePerUnit.Value,
        Timestamp = transaction.Timestamp,
    };
}
