using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Domain.Ships.Results;
using Wolfe.SpaceTraders.Sdk.Models.Ships;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersShipSellResultExtensions
{
    public static ShipSellResult ToDomain(this SpaceTradersShipSellResult result, IShipClient client) => new()
    {
        Agent = result.Agent.ToDomain(),
        Cargo = result.Cargo.ToDomain(client),
        Transaction = result.Transaction.ToDomain()
    };
}