﻿using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Ships.Results;
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