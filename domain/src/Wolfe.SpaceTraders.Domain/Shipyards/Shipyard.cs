using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Domain.Shipyards;

public class Shipyard : Waypoint
{
    public required List<ShipyardShipType> ShipTypes { get; init; } = [];
    public List<ShipyardTransaction> Transactions { get; init; } = [];
    public List<ShipyardShip> Ships { get; init; } = [];
}