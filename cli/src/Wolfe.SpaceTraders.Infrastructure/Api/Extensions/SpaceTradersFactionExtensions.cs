using Wolfe.SpaceTraders.Domain;
using Wolfe.SpaceTraders.Domain.Waypoints;
using Wolfe.SpaceTraders.Sdk.Models.Agents;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersFactionExtensions
{
    public static Faction ToDomain(this SpaceTradersFaction faction) => new()
    {
        Symbol = new FactionSymbol(faction.Symbol),
        Name = faction.Name,
        Description = faction.Description,
        Headquarters = new WaypointSymbol(faction.Headquarters),
    };
}