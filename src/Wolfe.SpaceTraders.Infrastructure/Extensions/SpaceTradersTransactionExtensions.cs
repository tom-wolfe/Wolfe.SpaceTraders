using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Sdk.Models;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersTransactionExtensions
{
    public static MarketplaceTransaction ToDomain(this SpaceTradersTransaction transaction) => new()
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