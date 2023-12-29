using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Sdk.Models.Navigation;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersLocationExtensions
{
    public static WaypointLocation ToDomain(this SpaceTradersWaypointLocation location) => new()
    {
        Type = new WaypointType(location.Type),
        Id = new WaypointId(location.Symbol),
        SystemId = new SystemId(location.SystemSymbol),
        Point = new Point(location.X, location.Y),
    };
}