using Wolfe.SpaceTraders.Domain.General;

namespace Wolfe.SpaceTraders.Domain.Marketplaces;

public record MarketTradeRoute(
    ItemId TradeItem,
    Marketplace ImportDestination,
    Marketplace ExportDestination,
    MarketTradeGood Import,
    MarketTradeGood Export
)
{
    public Distance Distance => ImportDestination.Location.DistanceTo(ExportDestination.Location);
    public double ProfitScore => (Export.SellPrice - Import.PurchasePrice).Value * 10 - Distance.Total;
    public int SupplyScore => Score(Export.Supply) - Score(Import.Supply);

    private static int Score(MarketTradeSupply supply)
    {
        if (supply == MarketTradeSupply.Scarce) { return 0; }
        if (supply == MarketTradeSupply.Limited) { return 1; }
        if (supply == MarketTradeSupply.Moderate) { return 2; }
        if (supply == MarketTradeSupply.High) { return 3; }
        if (supply == MarketTradeSupply.Abundant) { return 4; }
        throw new ArgumentOutOfRangeException(nameof(supply), supply, null);
    }
};