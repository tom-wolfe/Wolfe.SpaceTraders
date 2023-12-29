using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.General;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Domain.Shipyards;
using Wolfe.SpaceTraders.Infrastructure.Data.Models;

namespace Wolfe.SpaceTraders.Infrastructure.Data.Mapping;

internal static class Shipyards
{
    public static DataShipyard ToData(this Shipyard shipyard) => new()
    {
        Id = shipyard.Id.Value,
        Type = shipyard.Type.Value,
        Location = shipyard.Location.ToData(),
        Traits = shipyard.Traits.Select(t => t.ToData()).ToList(),
        ShipTypes = shipyard.ShipTypes.Select(t => t.ToData()).ToList(),
        Ships = shipyard.Ships.Select(t => t.ToData()).ToList(),
        Transactions = shipyard.Transactions.Select(t => t.ToData()).ToList(),
    };

    public static Shipyard ToDomain(this DataShipyard shipyard) => new()
    {
        Id = new WaypointId(shipyard.Id),
        Type = new WaypointType(shipyard.Type),
        Location = shipyard.Location.ToDomain(),
        Traits = shipyard.Traits.Select(t => t.ToDomain()).ToList(),
        ShipTypes = shipyard.ShipTypes.Select(t => t.ToDomain()).ToList(),
        Ships = shipyard.Ships.Select(t => t.ToDomain()).ToList(),
        Transactions = shipyard.Transactions.Select(t => t.ToDomain()).ToList(),
    };

    private static DataShipyardShipType ToData(this ShipyardShipType type) => new()
    {
        Type = type.Type.Value,
    };

    private static ShipyardShipType ToDomain(this DataShipyardShipType type) => new()
    {
        Type = new ShipType(type.Type),
    };

    private static DataShipyardShip ToData(this ShipyardShip ship) => new()
    {
        Type = ship.Type.Value,
        Name = ship.Name,
        Description = ship.Description,
        PurchasePrice = ship.PurchasePrice.Value,
    };

    private static ShipyardShip ToDomain(this DataShipyardShip ship) => new()
    {
        Type = new ShipType(ship.Type),
        Name = ship.Name,
        Description = ship.Description,
        PurchasePrice = new Credits(ship.PurchasePrice),
    };

    private static DataShipyardTransaction ToData(this ShipyardTransaction transaction) => new()
    {
        AgentId = transaction.AgentId.Value,
        Price = transaction.Price.Value,
        ShipId = transaction.ShipId.Value,
        Timestamp = transaction.Timestamp,
        WaypointId = transaction.WaypointId.Value
    };

    private static ShipyardTransaction ToDomain(this DataShipyardTransaction transaction) => new()
    {
        AgentId = new AgentId(transaction.AgentId),
        Price = new Credits(transaction.Price),
        ShipId = new ShipId(transaction.ShipId),
        Timestamp = transaction.Timestamp,
        WaypointId = new WaypointId(transaction.WaypointId)
    };
}
