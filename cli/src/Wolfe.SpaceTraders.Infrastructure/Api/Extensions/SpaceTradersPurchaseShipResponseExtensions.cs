using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Sdk.Responses;
using Wolfe.SpaceTraders.Service.Results;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersPurchaseShipResponseExtensions
{
    public static PurchaseShipResult ToDomain(this SpaceTradersPurchaseShipResponse response, IShipClient client) => new()
    {
        Agent = response.Agent.ToDomain(),
        Ship = response.Ship.ToDomain(client),
        Transaction = response.Transaction.ToDomain(),
    };
}