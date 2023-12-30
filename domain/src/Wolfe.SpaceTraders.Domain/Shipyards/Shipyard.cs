using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Domain.Shipyards;

public class Shipyard : Waypoint
{
    public required List<ShipyardShipType> ShipTypes { get; init; } = [];
    public bool IsSelling(ShipType type) => ShipTypes.Any(t => t.Type == type);
}