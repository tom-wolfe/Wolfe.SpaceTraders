using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Domain.Missions;

public interface IMissionService
{
    public Task<IMission> CreateProbeMission(Ship ship, CancellationToken cancellationToken = default);
    public IEnumerable<IMission> GetRunningMissions();
    public ValueTask<IMission?> GetMission(MissionId missionId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<IMission> GetMissions(CancellationToken cancellationToken = default);
    public ValueTask ResumeSuspendedMissions(CancellationToken cancellationToken = default);
    public ValueTask StopRunningMissions(CancellationToken cancellationToken = default);
}