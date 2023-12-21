using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Domain.Models.Navigation;
using Wolfe.SpaceTraders.Sdk.Models.Navigation;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersNavigationExtensions
{
    public static Navigation ToDomain(this SpaceTradersNavigation navigation) => new()
    {
        SystemSymbol = new SystemSymbol(navigation.SystemSymbol),
        WaypointSymbol = new WaypointSymbol(navigation.WaypointSymbol),
        Status = new NavigationStatus(navigation.Status),
        FlightMode = new FlightMode(navigation.FlightMode),
        Route = navigation.Route.ToDomain()
    };
}