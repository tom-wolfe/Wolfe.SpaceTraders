using Wolfe.SpaceTraders.Domain.Marketplace;
using Wolfe.SpaceTraders.Domain.Waypoints;
using Wolfe.SpaceTraders.Sdk.Models.Marketplace;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersMarketExtensions
{
    public static Market ToDomain(this SpaceTradersMarket market) => new()
    {
        Symbol = new WaypointSymbol(market.Symbol),
        Imports = market.Imports.Select(i => i.ToDomain()).ToList(),
        Exports = market.Exports.Select(e => e.ToDomain()).ToList(),
        Exchange = market.Exchange.Select(e => e.ToDomain()).ToList()
    };
}