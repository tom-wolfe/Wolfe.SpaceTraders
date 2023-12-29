using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Sdk.Models.Marketplace;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersMarketExtensions
{
    public static Marketplace ToDomain(this SpaceTradersMarketplace marketplace, Waypoint waypoint) => new()
    {
        Id = new WaypointId(marketplace.Symbol),
        Type = waypoint.Type,
        Location = waypoint.Location,
        Traits = [.. waypoint.Traits],
        Imports = marketplace.Imports.Select(i => i.ToDomain()).ToList(),
        Exports = marketplace.Exports.Select(e => e.ToDomain()).ToList(),
        Exchange = marketplace.Exchange.Select(e => e.ToDomain()).ToList()
    };
}