using Wolfe.SpaceTraders.Domain.Ships.Results;
using Wolfe.SpaceTraders.Sdk.Models.Ships;

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