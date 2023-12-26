using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Domain.Systems;
using Wolfe.SpaceTraders.Domain.Waypoints;
using Wolfe.SpaceTraders.Sdk.Models.Systems;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersWaypointExtensions
{
    public static Waypoint ToDomain(this SpaceTradersWaypoint waypoint) => new()
    {
        Symbol = new WaypointSymbol(waypoint.Symbol),
        Type = new WaypointType(waypoint.Type),
        SystemSymbol = new SystemSymbol(waypoint.SystemSymbol),
        Location = new Point(waypoint.X, waypoint.Y),
        Traits = waypoint.Traits.Select(t => t.ToDomain()).ToList(),
    };
}