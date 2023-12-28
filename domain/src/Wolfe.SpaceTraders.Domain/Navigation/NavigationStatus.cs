namespace Wolfe.SpaceTraders.Domain.Navigation;

[StronglyTypedId]
public partial struct NavigationStatus
{
    public static NavigationStatus Docked => new("DOCKED");
    public static NavigationStatus InOrbit => new("IN_ORBIT");
    public static NavigationStatus InTransit => new("IN_TRANSIT");
}