namespace Wolfe.SpaceTraders.Sdk.Models.Marketplace;

public class SpaceTradersMarketplace
{
    public required string Symbol { get; set; }
    public required IReadOnlyCollection<SpaceTradersMarketplaceItem> Imports { get; set; }
    public required IReadOnlyCollection<SpaceTradersMarketplaceItem> Exports { get; set; }
    public required IReadOnlyCollection<SpaceTradersMarketplaceItem> Exchange { get; set; }
}