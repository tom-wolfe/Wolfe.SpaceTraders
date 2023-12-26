﻿using Wolfe.SpaceTraders.Domain.Marketplace;

namespace Wolfe.SpaceTraders.Domain.Extraction;

public class ExtractionYield
{
    public TradeSymbol Symbol { get; init; }
    public int Units { get; init; }
}