using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Fleet.Commands;
using Wolfe.SpaceTraders.Domain.Fleet.Results;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Sdk.Requests;
using Wolfe.SpaceTraders.Sdk.Responses;

namespace Wolfe.SpaceTraders.Infrastructure.Api;

internal static class Fleets
{
    public static SpaceTradersPurchaseShipRequest ToApi(this PurchaseShipCommand command) => new()
    {
        Waypoint = command.ShipyardId.Value,
        ShipType = command.ShipType.Value,
    };

    public static PurchaseShipResult ToDomain(this SpaceTradersPurchaseShipResponse response, IShipClient client) => new()
    {
        Agent = response.Agent.ToDomain(),
        Ship = response.Ship.ToDomain(new AgentId(response.Agent.Symbol), client),
        Transaction = response.Transaction.ToDomain(),
    };
}