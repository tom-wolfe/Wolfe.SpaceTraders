using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Domain.Systems;
using Wolfe.SpaceTraders.Domain.Waypoints;
using Wolfe.SpaceTraders.Sdk.Models.Ships;
using Wolfe.SpaceTraders.Service.Results;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersPatchShipNavResultExtensions
{
    public static SetShipSpeedResult ToDomain(this SpaceTradersPatchShipNavResult result) => new()
    {
        SystemId = new SystemId(result.SystemSymbol),
        WaypointId = new WaypointId(result.WaypointSymbol),
        Status = new NavigationStatus(result.Status),
        Speed = new FlightSpeed(result.FlightMode),
        Route = result.Route.ToDomain()
    };
}