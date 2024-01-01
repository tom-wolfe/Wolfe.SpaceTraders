using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections;

namespace Wolfe.SpaceTraders.Infrastructure.Missions.Models;

internal class MongoMissionLogData
{
    [BsonId]
    public required ObjectId Id { get; init; }
    public required string MissionId { get; init; }
    public required string Message { get; set; }
    public required string Template { get; init; }
    public required IDictionary Data { get; set; }
    public MongoMissionLogError? Error { get; set; }
}

internal class MongoMissionLogError
{
    public required string Type { get; init; }
    public required string Message { get; set; }
    public required string StackTrace { get; set; }
}
