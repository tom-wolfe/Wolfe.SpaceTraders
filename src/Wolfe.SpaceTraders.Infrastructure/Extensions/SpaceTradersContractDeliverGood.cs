﻿using Wolfe.SpaceTraders.Core.Models;
using Wolfe.SpaceTraders.Sdk.Models.Contracts;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersContractDeliverGoodExtensions
{
    public static ContractGood ToDomain(this SpaceTradersContractGood good) => new()
    {
        DestinationSymbol = new WaypointSymbol(good.DestinationSymbol),
        TradeSymbol = new TradeSymbol(good.TradeSymbol),
        UnitsFulfilled = good.UnitsFulfilled,
        UnitsRequired = good.UnitsRequired,
    };
}