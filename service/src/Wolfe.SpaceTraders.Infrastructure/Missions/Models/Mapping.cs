using Wolfe.SpaceTraders.Domain.Missions;

namespace Wolfe.SpaceTraders.Infrastructure.Missions.Models;

internal static class Mapping
{
    public static MongoMission ToMongo(this IMission mission) => new()
    {
        Id = mission.Id.Value,
        Type = mission.Type.Value,
        AgentId = mission.AgentId.Value,
        ShipId = mission.ShipId.Value,
        Status = mission.Status.Value
    };
}
