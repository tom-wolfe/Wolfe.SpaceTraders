using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Marketplace;
using Wolfe.SpaceTraders.Sdk.Models.Contracts;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersContractDeliverGoodExtensions
{
    public static ContractItem ToDomain(this SpaceTradersContractGood good) => new()
    {
        DestinationId = new WaypointId(good.DestinationSymbol),
        ItemId = new ItemId(good.TradeSymbol),
        QuantityFulfilled = good.UnitsFulfilled,
        QuantityRequired = good.UnitsRequired,
    };
}