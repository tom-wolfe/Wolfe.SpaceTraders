using Wolfe.SpaceTraders.Domain.Marketplace;
using Wolfe.SpaceTraders.Domain.Waypoints;
using Wolfe.SpaceTraders.Sdk.Models.Marketplace;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersMarketExtensions
{
    public static Marketplace ToDomain(this SpaceTradersMarketplace marketplace, Waypoint waypoint) => new()
    {
        Symbol = new WaypointSymbol(marketplace.Symbol),
        Type = waypoint.Type,
        SystemSymbol = waypoint.SystemSymbol,
        Location = waypoint.Location,
        Traits = [.. waypoint.Traits],
        Imports = marketplace.Imports.Select(i => i.ToDomain()).ToList(),
        Exports = marketplace.Exports.Select(e => e.ToDomain()).ToList(),
        Exchange = marketplace.Exchange.Select(e => e.ToDomain()).ToList()
    };
}