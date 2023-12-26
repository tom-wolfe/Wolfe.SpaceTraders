using Wolfe.SpaceTraders.Sdk.Models.Ships;
using Wolfe.SpaceTraders.Service.Results;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersShipSellResultExtensions
{
    public static ShipSellResult ToDomain(this SpaceTradersShipSellResult result) => new()
    {
        Agent = result.Agent.ToDomain(),
        Cargo = result.Cargo.ToDomain(),
        Transaction = result.Transaction.ToDomain()
    };
}