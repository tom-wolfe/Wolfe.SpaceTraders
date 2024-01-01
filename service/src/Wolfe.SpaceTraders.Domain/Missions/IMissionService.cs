using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Domain.Missions;

public interface IMissionService
{
    public IMission CreateProbeMission(Ship ship);
    public IAsyncEnumerable<IMission> GetMissions(CancellationToken cancellationToken = default);
}