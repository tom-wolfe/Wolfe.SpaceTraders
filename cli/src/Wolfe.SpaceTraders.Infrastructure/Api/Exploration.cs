using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.General;
using Wolfe.SpaceTraders.Sdk.Models.Systems;

namespace Wolfe.SpaceTraders.Infrastructure.Api;

internal static class Exploration
{
    public static StarSystem ToDomain(this SpaceTradersSystem system) => new()
    {
        Id = new SystemId(system.Symbol),
        Type = new SystemType(system.Type),
        Location = new Point(system.X, system.Y),
    };

    public static Waypoint ToDomain(this SpaceTradersWaypoint waypoint) => new()
    {
        Id = new WaypointId(waypoint.Symbol),
        Type = new WaypointType(waypoint.Type),
        Location = new Point(waypoint.X, waypoint.Y),
        Traits = waypoint.Traits.Select(t => t.ToDomain()).ToList(),
    };

    public static WaypointTrait ToDomain(this SpaceTradersWaypointTrait trait) => new()
    {
        Id = new WaypointTraitId(trait.Symbol),
        Name = trait.Name,
        Description = trait.Description,
    };
}
