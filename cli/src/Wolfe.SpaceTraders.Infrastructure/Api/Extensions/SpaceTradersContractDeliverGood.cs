using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Domain.Marketplace;
using Wolfe.SpaceTraders.Domain.Waypoints;
using Wolfe.SpaceTraders.Sdk.Models.Contracts;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersContractDeliverGoodExtensions
{
    public static ContractGood ToDomain(this SpaceTradersContractGood good) => new()
    {
        DestinationId = new WaypointId(good.DestinationSymbol),
        TradeId = new TradeId(good.TradeSymbol),
        QuantityFulfilled = good.UnitsFulfilled,
        QuantityRequired = good.UnitsRequired,
    };
}