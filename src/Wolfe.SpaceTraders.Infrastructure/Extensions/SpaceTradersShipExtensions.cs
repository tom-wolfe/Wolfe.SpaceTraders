﻿using Wolfe.SpaceTraders.Core.Models;
using Wolfe.SpaceTraders.Sdk.Models.Ships;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersShipExtensions
{
    public static Ship ToDomain(this SpaceTradersShip ship) => new()
    {
        Symbol = new ShipSymbol(ship.Symbol),
        Navigation = ship.Nav.ToDomain(),
    };
}