using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Wolfe.SpaceTraders.Infrastructure.Missions.Models;

internal class MongoMissionLogData
{
    [BsonId]
    public required ObjectId Id { get; init; }

    public required string MissionId { get; init; }

    public required string Message { get; init; }
}
