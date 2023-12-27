using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Domain.Waypoints;
using Wolfe.SpaceTraders.Sdk.Models.Navigation;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersNavigationExtensions
{
    public static Navigation ToDomain(this SpaceTradersNavigation navigation) => new()
    {
        WaypointId = new WaypointId(navigation.WaypointSymbol),
        Status = new NavigationStatus(navigation.Status),
        Speed = new FlightSpeed(navigation.FlightMode),
        Route = navigation.Route.ToDomain()
    };
}