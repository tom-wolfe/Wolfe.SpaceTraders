﻿using Wolfe.SpaceTraders.Core.Models;
using Wolfe.SpaceTraders.Sdk.Models;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

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