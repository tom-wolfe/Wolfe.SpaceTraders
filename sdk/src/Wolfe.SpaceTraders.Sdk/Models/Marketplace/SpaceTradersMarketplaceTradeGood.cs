namespace Wolfe.SpaceTraders.Sdk.Models.Marketplace;

public class SpaceTradersMarketplaceTradeGood
{
    public required string Symbol { get; set; }
    public required string Type { get; set; }
    public required int TradeVolume { get; set; }
    public required string Supply { get; set; }
    public required string Activity { get; set; }
    public required int PurchasePrice { get; set; }
    public required int SellPrice { get; set; }
}