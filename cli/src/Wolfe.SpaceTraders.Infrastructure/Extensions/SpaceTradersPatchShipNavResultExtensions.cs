using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Domain.Systems;
using Wolfe.SpaceTraders.Domain.Waypoints;
using Wolfe.SpaceTraders.Sdk.Models.Ships;
using Wolfe.SpaceTraders.Service.Results;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersPatchShipNavResultExtensions
{
    public static SetShipSpeedResult ToDomain(this SpaceTradersPatchShipNavResult result) => new()
    {
        SystemSymbol = new SystemSymbol(result.SystemSymbol),
        WaypointSymbol = new WaypointSymbol(result.WaypointSymbol),
        Status = new NavigationStatus(result.Status),
        Speed = new FlightSpeed(result.FlightMode),
        Route = result.Route.ToDomain()
    };
}