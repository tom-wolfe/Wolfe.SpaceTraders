using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Sdk.Models.Navigation;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersNavigationRouteExtensions
{
    public static NavigationRoute ToDomain(this SpaceTradersNavigationRoute route) => new()
    {
        Arrival = route.Arrival,
        Origin = route.Origin.ToDomain(),
        Destination = route.Destination.ToDomain(),
        DepartureTime = route.DepartureTime,
    };
}