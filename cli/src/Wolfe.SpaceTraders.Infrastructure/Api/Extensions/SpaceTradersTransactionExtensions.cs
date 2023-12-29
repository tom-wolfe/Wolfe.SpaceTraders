using Wolfe.SpaceTraders.Domain;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Sdk.Models.Marketplace;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersTransactionExtensions
{
    public static Transaction ToDomain(this SpaceTradersTransaction transaction) => new()
    {
        ShipId = new ShipId(transaction.ShipSymbol),
        Timestamp = transaction.Timestamp,
        WaypointId = new WaypointId(transaction.WaypointSymbol),
        Type = new TransactionType(transaction.Type),
        PricePerUnit = new Credits(transaction.PricePerUnit),
        ItemId = new ItemId(transaction.TradeSymbol),
        Quantity = transaction.Units,
    };
}