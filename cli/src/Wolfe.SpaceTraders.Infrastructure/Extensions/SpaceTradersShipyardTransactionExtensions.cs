using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Domain.Models.Agents;
using Wolfe.SpaceTraders.Domain.Models.Ships;
using Wolfe.SpaceTraders.Domain.Models.Shipyards;
using Wolfe.SpaceTraders.Domain.Models.Waypoints;
using Wolfe.SpaceTraders.Sdk.Models.Shipyards;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersShipyardTransactionExtensions
{
    public static ShipyardTransaction ToDomain(this SpaceTradersShipyardTransaction transaction) => new()
    {
        AgentSymbol = new AgentSymbol(transaction.AgentSymbol),
        Price = new Credits(transaction.Price),
        ShipSymbol = new ShipSymbol(transaction.ShipSymbol),
        Timestamp = transaction.Timestamp,
        WaypointSymbol = new WaypointSymbol(transaction.WaypointSymbol),
    };
}