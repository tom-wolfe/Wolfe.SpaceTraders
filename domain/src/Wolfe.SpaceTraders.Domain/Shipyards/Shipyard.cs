using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Domain.Shipyards;

public class Shipyard
{
    public required WaypointSymbol Symbol { get; set; }
    public required List<ShipyardShipType> ShipTypes { get; set; } = [];
    public List<ShipyardTransaction> Transactions { get; set; } = [];
    public List<ShipyardShip> Ships { get; set; } = [];
}