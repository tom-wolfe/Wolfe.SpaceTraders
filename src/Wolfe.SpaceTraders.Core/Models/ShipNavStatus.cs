namespace Wolfe.SpaceTraders.Core.Models;

[StronglyTypedId]
public partial struct ShipNavStatus
{
    public static ShipNavStatus InTransit => new("IN_TRANSIT");
}