using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Sdk.Models;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersWaypointExtensions
{
    public static Waypoint ToDomain(this SpaceTradersWaypoint waypoint) => new()
    {
        Symbol = new WaypointSymbol(waypoint.Symbol),
        Type = new WaypointType(waypoint.Type),
        SystemSymbol = new SystemSymbol(waypoint.SystemSymbol),
        X = waypoint.X,
        Y = waypoint.Y,
    };
}