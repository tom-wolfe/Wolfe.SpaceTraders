using Wolfe.SpaceTraders.Domain.Fleet;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Missions;
using Wolfe.SpaceTraders.Domain.Missions.Scheduling;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Infrastructure.Missions.Models;

namespace Wolfe.SpaceTraders.Infrastructure.Missions;

internal class MissionFactory(
    IMissionLogFactory logFactory,
    IFleetService fleetService,
    IMarketplaceService marketplaceService,
    IMarketPriorityService marketPriorityService,
    IMissionScheduler missionScheduler,
    IMissionStore missionStore
) : IMissionFactory
{
    public IMission CreateProbeMission(Ship ship)
    {
        var missionId = MissionId.Generate();
        return ConstructProbeMission(missionId, ship, MissionStatus.New);
    }

    public async Task<IMission> Rehydrate(MongoMission mongoMission, CancellationToken cancellationToken = default)
    {
        var missionId = new MissionId(mongoMission.Id);
        var shipId = new ShipId(mongoMission.ShipId);
        var ship = await fleetService.GetShip(shipId, cancellationToken) ?? throw new Exception("Attempted to rehydrate mission for non-existent ship.");

        if (mongoMission.Type == MissionType.Probe.Value)
        {
            return ConstructProbeMission(missionId, ship, new MissionStatus(mongoMission.Status));
        }

        throw new NotSupportedException($"Mission type {mongoMission.Type} is not supported.");
    }

    private ProbeMission ConstructProbeMission(MissionId missionId, Ship ship, MissionStatus missionStatus)
    {
        var mission = new ProbeMission(missionStatus, logFactory.CreateLog(missionId), ship, marketplaceService, marketPriorityService, missionScheduler)
        {
            Id = missionId,
            AgentId = ship.AgentId,
        };
        mission.StatusChanged.Subscribe(_ => missionStore.UpdateMission(mission, CancellationToken.None));
        return mission;
    }
}
