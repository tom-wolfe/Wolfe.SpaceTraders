using Wolfe.SpaceTraders.Domain.Systems;
using Wolfe.SpaceTraders.Domain.Waypoints;
using Wolfe.SpaceTraders.Infrastructure.Data.Models;

namespace Wolfe.SpaceTraders.Infrastructure.Data.Mapping;

internal static class Waypoints
{
    public static DataWaypoint ToData(this Waypoint waypoint) => new()
    {
        Symbol = waypoint.Symbol.Value,
        Type = waypoint.Type.Value,
        System = waypoint.SystemSymbol.Value,
        Location = waypoint.Location.ToData(),
        Traits = waypoint.Traits.Select(t => t.ToData()).ToList()
    };

    public static Waypoint ToDomain(this DataWaypoint waypoint) => new()
    {
        Symbol = new WaypointSymbol(waypoint.Symbol),
        Type = new WaypointType(waypoint.Type),
        SystemSymbol = new SystemSymbol(waypoint.System),
        Location = waypoint.Location.ToDomain(),
        Traits = waypoint.Traits.Select(t => t.ToDomain()).ToList()
    };

    public static DataWaypointTrait ToData(this WaypointTrait trait) => new()
    {
        Symbol = trait.Symbol.Value,
        Name = trait.Name,
        Description = trait.Description
    };

    public static WaypointTrait ToDomain(this DataWaypointTrait trait) => new()
    {
        Symbol = new WaypointTraitSymbol(trait.Symbol),
        Name = trait.Name,
        Description = trait.Description
    };
}
