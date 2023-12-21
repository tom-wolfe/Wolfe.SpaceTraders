using Wolfe.SpaceTraders.Sdk.Responses;
using Wolfe.SpaceTraders.Service.Results;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersShipRefuelResponseExtensions
{
    public static ShipRefuelResult ToDomain(this SpaceTradersShipRefuelResponse response) => new()
    {
        Fuel = response.Fuel.ToDomain(),
        Agent = response.Agent.ToDomain(),
        Transaction = response.Transaction.ToDomain(),
    };
}