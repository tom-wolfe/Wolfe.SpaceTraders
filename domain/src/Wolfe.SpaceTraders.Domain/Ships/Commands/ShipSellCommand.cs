﻿using Wolfe.SpaceTraders.Domain.Marketplace;

namespace Wolfe.SpaceTraders.Domain.Ships.Commands;

public class ShipJettisonCommand
{
    public required ItemId ItemId { get; init; }
    public required int Quantity { get; init; }
}