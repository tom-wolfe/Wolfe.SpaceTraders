﻿using Wolfe.SpaceTraders.Domain.Marketplace;
using Wolfe.SpaceTraders.Sdk.Models.Marketplace;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersMarketItemExtensions
{
    public static MarketplaceItem ToDomain(this SpaceTradersMarketplaceItem item) => new()
    {
        ItemId = new ItemId(item.Symbol),
        Name = item.Name,
        Description = item.Description
    };
}