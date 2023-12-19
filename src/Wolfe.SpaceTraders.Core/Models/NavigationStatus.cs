namespace Wolfe.SpaceTraders.Core.Models;

[StronglyTypedId]
public partial struct NavigationStatus
{
    public static NavigationStatus InTransit => new("IN_TRANSIT");
}