using MongoDB.Bson.Serialization.Attributes;

namespace Wolfe.SpaceTraders.Infrastructure.Mongo.Models;

internal class MongoSystem
{
    [BsonId]
    public required string Id { get; init; }
    public required string Type { get; init; }
    public required MongoPoint Location { get; init; }
}
