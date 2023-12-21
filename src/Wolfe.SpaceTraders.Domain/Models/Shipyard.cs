namespace Wolfe.SpaceTraders.Domain.Models;

public class Shipyard
{
    public required WaypointSymbol Symbol { get; set; }
    public required List<ShipyardShipType> ShipTypes { get; set; } = [];
    public List<ShipyardTransaction> Transactions { get; set; } = [];
    public List<ShipyardShip> Ships { get; set; } = [];
}

public class ShipyardShipType
{
    public required ShipType Type { get; set; }
}