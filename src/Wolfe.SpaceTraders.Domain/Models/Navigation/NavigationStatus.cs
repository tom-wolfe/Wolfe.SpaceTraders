namespace Wolfe.SpaceTraders.Domain.Models;

[StronglyTypedId]
public partial struct NavigationStatus
{
    public static NavigationStatus InTransit => new("IN_TRANSIT");
}