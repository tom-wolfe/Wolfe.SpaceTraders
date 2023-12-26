using Wolfe.SpaceTraders.Domain.Marketplace;
using Wolfe.SpaceTraders.Domain.Waypoints;
using Wolfe.SpaceTraders.Sdk.Models.Marketplace;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersMarketExtensions
{
    public static Market ToDomain(this SpaceTradersMarket market, Waypoint waypoint) => new()
    {
        Symbol = new WaypointSymbol(market.Symbol),
        Type = waypoint.Type,
        SystemSymbol = waypoint.SystemSymbol,
        Location = waypoint.Location,
        Traits = [.. waypoint.Traits],
        Imports = market.Imports.Select(i => i.ToDomain()).ToList(),
        Exports = market.Exports.Select(e => e.ToDomain()).ToList(),
        Exchange = market.Exchange.Select(e => e.ToDomain()).ToList()
    };
}