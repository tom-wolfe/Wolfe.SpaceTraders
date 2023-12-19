using Wolfe.SpaceTraders.Sdk.Responses;
using Wolfe.SpaceTraders.Service.Responses;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersPurchaseShipResponseExtensions
{
    public static PurchaseShipResponse ToDomain(this SpaceTradersPurchaseShipResponse response) => new()
    {
        Agent = response.Agent.ToDomain(),
        Ship = response.Ship.ToDomain(),
        Transaction = response.Transaction.ToDomain(),
    };
}