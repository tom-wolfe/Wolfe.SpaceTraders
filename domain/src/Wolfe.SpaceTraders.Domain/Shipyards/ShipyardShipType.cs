using ShipType = Wolfe.SpaceTraders.Domain.Ships.ShipType;

namespace Wolfe.SpaceTraders.Domain.Shipyards;

public class ShipyardShipType
{
    public required ShipType Type { get; init; }
}