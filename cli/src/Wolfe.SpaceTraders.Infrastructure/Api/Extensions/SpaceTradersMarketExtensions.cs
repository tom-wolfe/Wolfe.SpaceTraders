﻿using Wolfe.SpaceTraders.Domain;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Sdk.Models.Marketplace;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersMarketExtensions
{
    public static Marketplace ToDomain(this SpaceTradersMarketplace marketplace, Waypoint waypoint) => new()
    {
        Id = new WaypointId(marketplace.Symbol),
        Type = waypoint.Type,
        Location = waypoint.Location,
        Traits = [.. waypoint.Traits],
        Imports = marketplace.Imports.Select(i => i.ToDomain()).ToList(),
        Exports = marketplace.Exports.Select(e => e.ToDomain()).ToList(),
        Exchange = marketplace.Exchange.Select(e => e.ToDomain()).ToList()
    };

    public static MarketData? ToMarketData(this SpaceTradersMarketplace marketplace)
    {
        if (marketplace.Transactions == null || marketplace.Transactions.Count == 0)
        {
            return null;
        }

        if (marketplace.TradeGoods == null || marketplace.TradeGoods.Count == 0)
        {
            return null;
        }

        return new MarketData
        {
            RetrievedAt = DateTimeOffset.UtcNow,
            WaypointId = new WaypointId(marketplace.Symbol),
            TradeGoods = marketplace.TradeGoods.Select(t => t.ToDomain()).ToList(),
            Transactions = marketplace.Transactions.Select(t => t.ToDomain()).ToList()
        };
    }

    public static MarketTradeGood ToDomain(this SpaceTradersMarketplaceTradeGood tradeGood) => new()
    {
        ItemId = new ItemId(tradeGood.Symbol),
        Type = new MarketTradeType(tradeGood.Type),
        Activity = new MarketTradeActivity(tradeGood.Activity),
        Supply = new MarketTradeSupply(tradeGood.Supply),
        Volume = tradeGood.TradeVolume,
        PurchasePrice = new Credits(tradeGood.PurchasePrice),
        SellPrice = new Credits(tradeGood.SellPrice)
    };

    public static MarketTransaction ToDomain(this SpaceTradersMarketplaceTransaction transaction) => new()
    {
        ShipId = new ShipId(transaction.ShipSymbol),
        ItemId = new ItemId(transaction.TradeSymbol),
        Type = new TransactionType(transaction.Type),
        PricePerUnit = new Credits(transaction.PricePerUnit),
        Quantity = transaction.Units,
        Timestamp = transaction.Timestamp,
    };
}