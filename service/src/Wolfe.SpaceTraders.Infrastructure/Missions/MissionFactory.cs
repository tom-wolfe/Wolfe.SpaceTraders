using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Fleet;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Missions;
using Wolfe.SpaceTraders.Domain.Missions.Scheduling;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Infrastructure.Missions.Models;

namespace Wolfe.SpaceTraders.Infrastructure.Missions;

internal class MissionFactory(
    IFleetService fleetService,
    IMarketplaceService marketplaceService,
    IMarketPriorityService marketPriorityService,
    IMissionScheduler missionScheduler,
    IWayfinderService wayfinder,
    IMissionStore missionStore,
    IEnumerable<IMissionLog> logs
) : IMissionFactory
{
    public IMission CreateProbeMission(Ship ship)
    {
        var missionId = MissionId.Generate();
        return ConstructProbeMission(missionId, ship, MissionStatus.New);
    }

    public IMission CreateTradingMission(Ship ship)
    {
        var missionId = MissionId.Generate();
        return ConstructTradingMission(missionId, ship, MissionStatus.New);
    }

    public async Task<IMission> Rehydrate(MongoMission mongoMission, CancellationToken cancellationToken = default)
    {
        var missionId = new MissionId(mongoMission.Id);
        var status = new MissionStatus(mongoMission.Status);
        var shipId = new ShipId(mongoMission.ShipId);
        var ship = await fleetService.GetShip(shipId, cancellationToken) ?? throw new Exception("Attempted to rehydrate mission for non-existent ship.");

        if (mongoMission.Type == MissionType.Probe.Value)
        {
            return ConstructProbeMission(missionId, ship, status);
        }
        if (mongoMission.Type == MissionType.Trading.Value)
        {
            return ConstructTradingMission(missionId, ship, status);
        }

        throw new NotSupportedException($"Mission type {mongoMission.Type} is not supported.");
    }

    private ProbeMission ConstructProbeMission(MissionId missionId, Ship ship, MissionStatus missionStatus)
    {
        var mission = new ProbeMission(missionStatus, ship, marketPriorityService, missionScheduler)
        {
            Id = missionId,
            AgentId = ship.AgentId,
        };
        WireUpMission(mission);
        return mission;
    }

    private TradingMission ConstructTradingMission(MissionId missionId, Ship ship, MissionStatus missionStatus)
    {
        var mission = new TradingMission(missionStatus, ship, marketplaceService, wayfinder, missionScheduler)
        {
            Id = missionId,
            AgentId = ship.AgentId,
        };
        WireUpMission(mission);
        return mission;
    }

    private void WireUpMission(IMission mission)
    {
        mission.StatusChanged.Subscribe(_ => missionStore.UpdateMission(mission, CancellationToken.None));
        foreach (var log in logs)
        {
            mission.StatusChanged.Subscribe(status => log.OnStatusChanged(mission, status));
            mission.Event.Subscribe(message => log.OnEvent(mission, message));
            mission.Error.Subscribe(error => log.OnError(mission, error));
        }
    }
}
