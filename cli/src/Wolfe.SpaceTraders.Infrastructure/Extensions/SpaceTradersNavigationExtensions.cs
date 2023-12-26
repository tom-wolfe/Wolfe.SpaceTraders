using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Domain.Systems;
using Wolfe.SpaceTraders.Domain.Waypoints;
using Wolfe.SpaceTraders.Sdk.Models.Navigation;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersNavigationExtensions
{
    public static Navigation ToDomain(this SpaceTradersNavigation navigation) => new()
    {
        SystemSymbol = new SystemSymbol(navigation.SystemSymbol),
        WaypointSymbol = new WaypointSymbol(navigation.WaypointSymbol),
        Status = new NavigationStatus(navigation.Status),
        Speed = new FlightSpeed(navigation.FlightMode),
        Route = navigation.Route.ToDomain()
    };
}