using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Infrastructure.Mongo.Models;

namespace Wolfe.SpaceTraders.Infrastructure.Mongo.Mapping;

internal static class Waypoints
{
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
}
