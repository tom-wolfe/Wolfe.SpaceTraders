﻿using Wolfe.SpaceTraders.Domain.Marketplace;

namespace Wolfe.SpaceTraders.Domain.Extraction;

public class ExtractionYield
{
    public TradeId TradeId { get; init; }
    public int Quantity { get; init; }
}