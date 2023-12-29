using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Sdk.Models.Systems;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersWaypointExtensions
{
    public static Waypoint ToDomain(this SpaceTradersWaypoint waypoint) => new()
    {
        Id = new WaypointId(waypoint.Symbol),
        Type = new WaypointType(waypoint.Type),
        Location = new Point(waypoint.X, waypoint.Y),
        Traits = waypoint.Traits.Select(t => t.ToDomain()).ToList(),
    };
}