using Wolfe.SpaceTraders.Domain.Extraction;
using Wolfe.SpaceTraders.Domain.Marketplace;
using Wolfe.SpaceTraders.Sdk.Models.Extraction;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersExtractionYieldExtensions
{
    public static ExtractionYield ToDomain(this SpaceTradersExtractionYield yield) => new()
    {
        ItemId = new ItemId(yield.Symbol),
        Quantity = yield.Units,
    };
}