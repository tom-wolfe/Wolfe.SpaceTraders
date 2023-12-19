using Wolfe.SpaceTraders.Sdk.Responses;
using Wolfe.SpaceTraders.Service.Responses;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersShipRefuelResponseExtensions
{
    public static ShipRefuelResponse ToDomain(this SpaceTradersShipRefuelResponse response) => new()
    {
        Fuel = response.Fuel.ToDomain(),
        Agent = response.Agent.ToDomain(),
        Transaction = response.Transaction.ToDomain(),
    };
}