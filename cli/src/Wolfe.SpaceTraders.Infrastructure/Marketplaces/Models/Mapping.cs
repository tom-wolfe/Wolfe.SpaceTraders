using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Infrastructure.Exploration.Models;

namespace Wolfe.SpaceTraders.Infrastructure.Marketplaces.Models;

internal static class Mapping
{
    public static MongoMarketplace ToMongo(this Marketplace marketplace) => new()
    {
        Id = marketplace.Id.Value,
        SystemId = marketplace.Id.SystemId.Value,
        Type = marketplace.Type.Value,
        Location = marketplace.Location.ToMongo(),
        Traits = marketplace.Traits.Select(t => t.ToMongo()).ToList(),
        Imports = marketplace.Imports.Select(i => i.ToMongo()).ToList(),
        Exports = marketplace.Exports.Select(i => i.ToMongo()).ToList(),
        Exchange = marketplace.Exchange.Select(i => i.ToMongo()).ToList(),
    };

    public static Marketplace ToDomain(this MongoMarketplace marketplace) => new()
    {
        Id = new WaypointId(marketplace.Id),
        Type = new WaypointType(marketplace.Type),
        Location = marketplace.Location.ToDomain(),
        Traits = marketplace.Traits.Select(t => t.ToDomain()).ToList(),
        Imports = marketplace.Imports.Select(i => i.ToDomain()).ToList(),
        Exports = marketplace.Exports.Select(i => i.ToDomain()).ToList(),
        Exchange = marketplace.Exchange.Select(i => i.ToDomain()).ToList(),
    };

    private static MongoMarketplaceItem ToMongo(this MarketplaceItem item) => new(item.ItemId.Value, item.Name, item.Description);

    private static MarketplaceItem ToDomain(this MongoMarketplaceItem item) => new()
    {
        ItemId = new ItemId(item.Id),
        Name = item.Name,
        Description = item.Description,
    };
}
