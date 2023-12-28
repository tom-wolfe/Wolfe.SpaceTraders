using Wolfe.SpaceTraders.Domain.Systems;
using Wolfe.SpaceTraders.Domain.Waypoints;
using Wolfe.SpaceTraders.Infrastructure.Data.Models;

namespace Wolfe.SpaceTraders.Infrastructure.Data.Mapping;

internal static class Waypoints
{
    public static DataWaypoint ToData(this Waypoint waypoint) => new()
    {
        Id = waypoint.Id.Value,
        Type = waypoint.Type.Value,
        System = waypoint.SystemId.Value,
        Location = waypoint.Location.ToData(),
        Traits = waypoint.Traits.Select(t => t.ToData()).ToList()
    };

    public static Waypoint ToDomain(this DataWaypoint waypoint) => new()
    {
        Id = new WaypointId(waypoint.Id),
        Type = new WaypointType(waypoint.Type),
        SystemId = new SystemId(waypoint.System),
        Location = waypoint.Location.ToDomain(),
        Traits = waypoint.Traits.Select(t => t.ToDomain()).ToList()
    };

    public static DataWaypointTrait ToData(this WaypointTrait trait) => new()
    {
        Id = trait.Id.Value,
        Name = trait.Name,
        Description = trait.Description
    };

    public static WaypointTrait ToDomain(this DataWaypointTrait trait) => new()
    {
        Id = new WaypointTraitId(trait.Id),
        Name = trait.Name,
        Description = trait.Description
    };
}
