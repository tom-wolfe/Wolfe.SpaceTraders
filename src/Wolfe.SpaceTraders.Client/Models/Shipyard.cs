namespace Wolfe.SpaceTraders.Models;

public class Shipyard
{
    public required WaypointSymbol Symbol { get; set; }
    public required List<ShipyardShipType> ShipTypes { get; set; } = new();
    public required List<ShipyardTransaction> Transactions { get; set; } = new();
    public required List<ShipyardShip> Ships { get; set; } = new();
}

public class ShipyardShipType
{
    public required ShipType Type { get; set; }
}