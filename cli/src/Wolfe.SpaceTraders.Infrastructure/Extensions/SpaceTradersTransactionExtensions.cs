using Wolfe.SpaceTraders.Domain;
using Wolfe.SpaceTraders.Domain.Marketplace;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Domain.Waypoints;
using Wolfe.SpaceTraders.Sdk.Models.Marketplace;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersTransactionExtensions
{
    public static Transaction ToDomain(this SpaceTradersTransaction transaction) => new()
    {
        ShipSymbol = new ShipSymbol(transaction.ShipSymbol),
        Timestamp = transaction.Timestamp,
        WaypointSymbol = new WaypointSymbol(transaction.WaypointSymbol),
        Type = new TransactionType(transaction.Type),
        PricePerUnit = new Credits(transaction.PricePerUnit),
        TotalPrice = new Credits(transaction.TotalPrice),
        TradeSymbol = new TradeSymbol(transaction.TradeSymbol),
        Units = transaction.Units,
    };
}