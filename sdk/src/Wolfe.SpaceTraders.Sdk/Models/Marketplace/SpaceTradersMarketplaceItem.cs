namespace Wolfe.SpaceTraders.Sdk.Models.Marketplace;

public class SpaceTradersMarketplaceItem
{
    /// <summary>
    /// The good's symbol.
    /// </summary>
    public required string Symbol { get; set; }

    /// <summary>
    /// The name of the good.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The description of the good.
    /// </summary>
    public required string Description { get; set; }
}