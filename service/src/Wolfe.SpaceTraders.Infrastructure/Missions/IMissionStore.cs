using Wolfe.SpaceTraders.Domain.Missions;
using Wolfe.SpaceTraders.Infrastructure.Missions.Models;

namespace Wolfe.SpaceTraders.Infrastructure.Missions;

internal interface IMissionStore
{
    public Task UpdateMission(MongoMission mission, CancellationToken cancellationToken = default);
    public Task<MongoMission?> GetMission(MissionId missionId, CancellationToken cancellationToken = default);
    IAsyncEnumerable<MongoMission> GetMissions(CancellationToken cancellationToken = default);
}