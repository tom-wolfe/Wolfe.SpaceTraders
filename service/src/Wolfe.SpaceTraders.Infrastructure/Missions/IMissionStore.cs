using Wolfe.SpaceTraders.Domain.Missions;

namespace Wolfe.SpaceTraders.Infrastructure.Missions;

internal interface IMissionStore
{
    public Task UpdateMission(IMission mission, CancellationToken cancellationToken = default);
    public Task<IMission?> GetMission(MissionId missionId, IMissionFactory factory, CancellationToken cancellationToken = default);
    IAsyncEnumerable<IMission> GetMissions(IMissionFactory factory, CancellationToken cancellationToken = default);
}