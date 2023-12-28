using Wolfe.SpaceTraders.Domain.Marketplace;
using Wolfe.SpaceTraders.Domain.Waypoints;
using Wolfe.SpaceTraders.Infrastructure.Data.Models;

namespace Wolfe.SpaceTraders.Infrastructure.Data.Mapping;

internal static class Marketplaces
{
    public static DataMarketplace ToData(this Marketplace marketplace) => new()
    {
        Id = marketplace.Id.Value,
        Type = marketplace.Type.Value,
        Location = marketplace.Location.ToData(),
        Traits = marketplace.Traits.Select(t => t.ToData()).ToList(),
        Imports = marketplace.Imports.Select(i => i.ToData()).ToList(),
        Exports = marketplace.Exports.Select(i => i.ToData()).ToList(),
        Exchange = marketplace.Exchange.Select(i => i.ToData()).ToList(),
    };

    public static Marketplace ToDomain(this DataMarketplace marketplace) => new()
    {
        Id = new WaypointId(marketplace.Id),
        Type = new WaypointType(marketplace.Type),
        Location = marketplace.Location.ToDomain(),
        Traits = marketplace.Traits.Select(t => t.ToDomain()).ToList(),
        Imports = marketplace.Imports.Select(i => i.ToDomain()).ToList(),
        Exports = marketplace.Exports.Select(i => i.ToDomain()).ToList(),
        Exchange = marketplace.Exchange.Select(i => i.ToDomain()).ToList(),
    };

    private static DataMarketplaceItem ToData(this MarketplaceItem item) => new()
    {
        Id = item.ItemId.Value,
        Name = item.Name,
        Description = item.Description,
    };

    private static MarketplaceItem ToDomain(this DataMarketplaceItem item) => new()
    {
        ItemId = new ItemId(item.Id),
        Name = item.Name,
        Description = item.Description,
    };
}
