using Wolfe.SpaceTraders.Sdk.Models.Ships;
using Wolfe.SpaceTraders.Service.Results;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersShipRefuelResultExtensions
{
    public static ShipRefuelResult ToDomain(this SpaceTradersShipRefuelResult result) => new()
    {
        Fuel = result.Fuel.ToDomain(),
        Agent = result.Agent.ToDomain(),
        Transaction = result.Transaction.ToDomain(),
    };
}