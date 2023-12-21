using Wolfe.SpaceTraders.Sdk.Responses;
using Wolfe.SpaceTraders.Service.Results;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersPurchaseShipResponseExtensions
{
    public static PurchaseShipResult ToDomain(this SpaceTradersPurchaseShipResponse response) => new()
    {
        Agent = response.Agent.ToDomain(),
        Ship = response.Ship.ToDomain(),
        Transaction = response.Transaction.ToDomain(),
    };
}