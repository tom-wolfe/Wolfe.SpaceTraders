﻿using Wolfe.SpaceTraders.Domain.Marketplaces;

namespace Wolfe.SpaceTraders.Domain.Ships.Results;

public class ExtractionYield
{
    public ItemId ItemId { get; init; }
    public int Quantity { get; init; }
}