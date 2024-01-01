using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Missions.Logs;
using Wolfe.SpaceTraders.Domain.Missions.Scheduling;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Domain.Missions;

internal class MissionService(
    IMissionLogFactory logFactory,
    IMarketplaceService marketplaceService,
    IMarketPriorityService marketPriorityService,
    IMissionScheduler missionScheduler
) : IMissionService
{
    private readonly List<IMission> _missions = [];

    public IMission CreateProbeMission(Ship ship)
    {
        if (_missions.Any(m => m.ShipId == ship.Id))
        {
            throw new InvalidOperationException("Ship has already been assigned a mission.");
        }

        var missionId = MissionId.Generate();
        var mission = new ProbeMission(logFactory.CreateLog(missionId), ship, marketplaceService, marketPriorityService, missionScheduler)
        {
            Id = missionId
        };

        _missions.Add(mission);
        return mission;
    }

    public IAsyncEnumerable<IMission> GetMissions(CancellationToken cancellationToken = default)
    {
        return _missions.ToAsyncEnumerable();
    }
}
