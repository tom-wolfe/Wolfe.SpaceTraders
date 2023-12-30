using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.General;

namespace Wolfe.SpaceTraders.Infrastructure.Exploration.Models;

internal static class Mapping
{
    public static MongoSystem ToMongo(this StarSystem system) => new()
    {
        Id = system.Id.Value,
        Type = system.Type.Value,
        Location = system.Location.ToMongo(),
    };

    public static StarSystem ToDomain(this MongoSystem system) => new()
    {
        Id = new SystemId(system.Id),
        Type = new SystemType(system.Type),
        Location = system.Location.ToDomain(),
    };

    public static MongoWaypoint ToMongo(this Waypoint waypoint) => new()
    {
        Id = waypoint.Id.Value,
        SystemId = waypoint.SystemId.Value,
        Type = waypoint.Type.Value,
        Location = waypoint.Location.ToMongo(),
        Traits = waypoint.Traits.Select(t => t.ToMongo()).ToList()
    };

    public static Waypoint ToDomain(this MongoWaypoint waypoint) => new()
    {
        Id = new WaypointId(waypoint.Id),
        Type = new WaypointType(waypoint.Type),
        Location = waypoint.Location.ToDomain(),
        Traits = waypoint.Traits.Select(t => t.ToDomain()).ToList()
    };

    public static MongoWaypointTrait ToMongo(this WaypointTrait trait) => new(trait.Id.Value, trait.Name, trait.Description);

    public static WaypointTrait ToDomain(this MongoWaypointTrait trait) => new()
    {
        Id = new WaypointTraitId(trait.Id),
        Name = trait.Name,
        Description = trait.Description
    };

    public static MongoPoint ToMongo(this Point point) => new(point.X, point.Y);
    public static Point ToDomain(this MongoPoint point) => new(point.X, point.Y);
}
