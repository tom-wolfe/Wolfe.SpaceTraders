using MongoDB.Bson.Serialization.Attributes;

namespace Wolfe.SpaceTraders.Infrastructure.Missions.Models;

internal class MongoMission
{
    [BsonId]
    public required string Id { get; set; }

    public required string Type { get; set; }

    public required string ShipId { get; set; }

    public required string AgentId { get; set; }

    public required string Status { get; set; }
}
