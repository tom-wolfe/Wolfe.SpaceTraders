namespace Wolfe.SpaceTraders.Domain.Navigation;

[StronglyTypedId]
public partial struct ShipNavigationStatus
{
    public static ShipNavigationStatus Docked => new("DOCKED");
    public static ShipNavigationStatus InOrbit => new("IN_ORBIT");
    public static ShipNavigationStatus InTransit => new("IN_TRANSIT");
}