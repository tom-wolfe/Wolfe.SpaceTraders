﻿using Wolfe.SpaceTraders.Sdk.Requests;
using Wolfe.SpaceTraders.Service.Commands;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class ShipNavigateRequestExtensions
{
    public static SpaceTradersShipNavigateRequest ToApi(this ShipNavigateCommand command) => new()
    {
        WaypointSymbol = command.WaypointSymbol.Value,
    };
}