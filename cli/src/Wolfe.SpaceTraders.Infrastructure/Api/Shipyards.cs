using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.General;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Domain.Shipyards;
using Wolfe.SpaceTraders.Sdk.Models.Shipyards;

namespace Wolfe.SpaceTraders.Infrastructure.Api;

internal static class Shipyards
{
    public static Shipyard ToDomain(this SpaceTradersShipyard shipyard, Waypoint waypoint) => new()
    {
        Id = new WaypointId(shipyard.Symbol),
        Type = waypoint.Type,
        Location = waypoint.Location,
        Traits = [.. waypoint.Traits],
        ShipTypes = shipyard.ShipTypes.Select(s => s.ToDomain()).ToList(),
        Ships = shipyard.Ships?.Select(s => s.ToDomain()).ToList() ?? new List<ShipyardShip>(),
        Transactions = shipyard.Transactions?.Select(s => s.ToDomain()).ToList() ?? new List<ShipyardTransaction>(),
    };

    public static ShipyardShip ToDomain(this SpaceTradersShipyardShip ship) => new()
    {
        Type = new ShipType(ship.Type),
        Description = ship.Description,
        Name = ship.Name,
        PurchasePrice = new Credits(ship.PurchasePrice)
    };

    public static ShipyardShipType ToDomain(this SpaceTradersShipyardShipType type) => new()
    {
        Type = new ShipType(type.Type)
    };

    public static ShipyardTransaction ToDomain(this SpaceTradersShipyardTransaction transaction) => new()
    {
        AgentId = new AgentId(transaction.AgentSymbol),
        Price = new Credits(transaction.Price),
        ShipId = new ShipId(transaction.ShipSymbol),
        Timestamp = transaction.Timestamp,
        WaypointId = new WaypointId(transaction.WaypointSymbol),
    };
}