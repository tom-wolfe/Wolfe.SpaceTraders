using Wolfe.SpaceTraders.Domain.Marketplaces;

namespace Wolfe.SpaceTraders.Domain.Extraction;

public class ExtractionYield
{
    public ItemId ItemId { get; init; }
    public int Quantity { get; init; }
}