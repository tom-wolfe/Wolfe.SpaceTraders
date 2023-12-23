namespace Wolfe.SpaceTraders.Domain.Navigation;

[StronglyTypedId]
public partial struct NavigationStatus
{
    public static NavigationStatus InTransit => new("IN_TRANSIT");
}