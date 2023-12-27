﻿using Wolfe.SpaceTraders.Domain.Marketplace;
using Wolfe.SpaceTraders.Domain.Systems;
using Wolfe.SpaceTraders.Domain.Waypoints;
using Wolfe.SpaceTraders.Infrastructure.Data.Models;

namespace Wolfe.SpaceTraders.Infrastructure.Data.Mapping;

internal static class Marketplaces
{
    public static DataMarketplace ToData(this Marketplace marketplace) => new()
    {
        Symbol = marketplace.Symbol.Value,
        Type = marketplace.Type.Value,
        System = marketplace.SystemSymbol.Value,
        Location = marketplace.Location.ToData(),
        Traits = marketplace.Traits.Select(t => t.ToData()).ToList(),
        Imports = marketplace.Imports.Select(i => i.ToData()).ToList(),
        Exports = marketplace.Exports.Select(i => i.ToData()).ToList(),
        Exchange = marketplace.Exchange.Select(i => i.ToData()).ToList(),
    };

    public static Marketplace ToDomain(this DataMarketplace marketplace) => new()
    {
        Symbol = new WaypointSymbol(marketplace.Symbol),
        Type = new WaypointType(marketplace.Type),
        SystemSymbol = new SystemSymbol(marketplace.System),
        Location = marketplace.Location.ToDomain(),
        Traits = marketplace.Traits.Select(t => t.ToDomain()).ToList(),
        Imports = marketplace.Imports.Select(i => i.ToDomain()).ToList(),
        Exports = marketplace.Exports.Select(i => i.ToDomain()).ToList(),
        Exchange = marketplace.Exchange.Select(i => i.ToDomain()).ToList(),
    };

    private static DataMarketplaceItem ToData(this MarketplaceItem item) => new()
    {
        Symbol = item.Symbol.Value,
        Name = item.Name,
        Description = item.Description,
    };

    private static MarketplaceItem ToDomain(this DataMarketplaceItem item) => new()
    {
        Symbol = new TradeSymbol(item.Symbol),
        Name = item.Name,
        Description = item.Description,
    };
}