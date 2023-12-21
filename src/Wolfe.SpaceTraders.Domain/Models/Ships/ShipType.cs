namespace Wolfe.SpaceTraders.Domain.Models;

[StronglyTypedId]
public partial struct ShipType
{
    public static ShipType MiningDrone => new("SHIP_MINING_DRONE");
}