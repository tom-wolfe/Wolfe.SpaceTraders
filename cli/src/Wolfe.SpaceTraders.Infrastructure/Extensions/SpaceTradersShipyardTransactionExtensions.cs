using Wolfe.SpaceTraders.Domain;
using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Domain.Shipyards;
using Wolfe.SpaceTraders.Domain.Waypoints;
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