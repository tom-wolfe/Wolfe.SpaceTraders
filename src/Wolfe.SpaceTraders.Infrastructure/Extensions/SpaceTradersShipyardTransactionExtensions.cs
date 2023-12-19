﻿using Wolfe.SpaceTraders.Core.Models;
using Wolfe.SpaceTraders.Sdk.Models.Shipyards;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersShipyardTransactionExtensions
{
    public static ShipyardTransaction ToDomain(this SpaceTradersShipyardTransaction transaction) => new()
    {
        AgentSymbol = new AgentSymbol(transaction.AgentSymbol),
        Price = new Credits(transaction.Price),
        ShipSymbol = new ShipSymbol(transaction.ShipSymbol),
        Timestamp = transaction.Timestamp,
        WaypointSymbol = new WaypointSymbol(transaction.WaypointSymbol),
    };
}