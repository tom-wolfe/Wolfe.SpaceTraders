namespace Wolfe.SpaceTraders.Sdk.Models.Marketplace;

public class SpaceTradersMarket
{
    public required string Symbol { get; set; }
    public required IReadOnlyCollection<SpaceTradersMarketItem> Imports { get; set; }
    public required IReadOnlyCollection<SpaceTradersMarketItem> Exports { get; set; }
    public required IReadOnlyCollection<SpaceTradersMarketItem> Exchange { get; set; }
}