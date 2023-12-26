using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Domain.Shipyards;

public class ShipyardShip
{
    public required ShipType Type { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required Credits PurchasePrice { get; init; }
}