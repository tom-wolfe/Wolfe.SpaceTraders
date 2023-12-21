﻿using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Domain.Models.Extraction;
using Wolfe.SpaceTraders.Sdk.Models.Extraction;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersExtractionYieldExtensions
{
    public static ExtractionYield ToDomain(this SpaceTradersExtractionYield yield) => new()
    {
        Symbol = new TradeSymbol(yield.Symbol),
        Units = yield.Units,
    };
}