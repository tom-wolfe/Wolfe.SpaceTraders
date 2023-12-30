using MongoDB.Bson.Serialization.Attributes;

namespace Wolfe.SpaceTraders.Infrastructure.Mongo.Models;

internal class MongoWaypoint
{
    [BsonId]
    public required string Id { get; set; }

    public required string SystemId { get; set; }

    public required string Type { get; set; }

    public required MongoPoint Location { get; set; }

    public required IReadOnlyCollection<MongoWaypointTrait> Traits { get; init; } = [];
}

internal record MongoWaypointTrait(string Id, string Name, string Description);