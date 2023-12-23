namespace Wolfe.SpaceTraders.Domain.Ships;

[StronglyTypedId]
public partial struct ShipType
{
    public static ShipType MiningDrone => new("SHIP_MINING_DRONE");
}