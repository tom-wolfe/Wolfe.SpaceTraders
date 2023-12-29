using Wolfe.SpaceTraders.Domain;
using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Domain.Shipyards;
using Wolfe.SpaceTraders.Sdk.Models.Shipyards;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersShipyardTransactionExtensions
{
    public static ShipyardTransaction ToDomain(this SpaceTradersShipyardTransaction transaction) => new()
    {
        AgentId = new AgentId(transaction.AgentSymbol),
        Price = new Credits(transaction.Price),
        ShipId = new ShipId(transaction.ShipSymbol),
        Timestamp = transaction.Timestamp,
        WaypointId = new WaypointId(transaction.WaypointSymbol),
    };
}