﻿using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Domain.Marketplace;

public class Marketplace : Waypoint
{
    public required List<MarketplaceItem> Imports { get; init; } = [];
    public required List<MarketplaceItem> Exports { get; init; } = [];
    public required List<MarketplaceItem> Exchange { get; init; } = [];

    public bool IsBuying(ItemId itemId) => Imports.Any(e => e.ItemId == itemId);
    public bool IsSelling(ItemId itemId) => Exports.Any(e => e.ItemId == itemId);
}