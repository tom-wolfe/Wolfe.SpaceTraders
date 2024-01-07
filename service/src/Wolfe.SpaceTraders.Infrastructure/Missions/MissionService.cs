using System.Runtime.CompilerServices;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Fleet;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Missions;
using Wolfe.SpaceTraders.Domain.Missions.Scheduling;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Infrastructure.Missions.Models;

namespace Wolfe.SpaceTraders.Infrastructure.Missions;

internal class MissionService(
    IMissionStore missionStore,
    IFleetService fleetService,
    IMarketPriorityService marketPriorityService,
    IMissionScheduler missionScheduler,
    IWayfinderService wayfinder,
    IEnumerable<IMissionLog> logs
) : IMissionService
{
    private List<IMission>? _missions;

    public async Task<IMission> CreateProbeMission(Ship ship, CancellationToken cancellationToken = default)
    {
        var missions = await GetMissions(cancellationToken).ToListAsync(cancellationToken);
        if (missions.Any(m => m.ShipId == ship.Id))
        {
            throw new InvalidOperationException("Ship has already been assigned a mission.");
        }

        var missionId = MissionId.Generate();
        var mission = ConstructProbeMission(missionId, ship, MissionStatus.New);

        _missions!.Add(mission);
        await missionStore.UpdateMission(mission.ToMongo(), cancellationToken);

        return mission;
    }

    public async Task<IMission> CreateTradingMission(Ship ship, CancellationToken cancellationToken = default)
    {
        var missions = await GetMissions(cancellationToken).ToListAsync(cancellationToken);
        if (missions.Any(m => m.ShipId == ship.Id))
        {
            throw new InvalidOperationException("Ship has already been assigned a mission.");
        }

        var missionId = MissionId.Generate();
        var mission = ConstructTradingMission(missionId, ship, MissionStatus.New);

        _missions!.Add(mission);
        await missionStore.UpdateMission(mission.ToMongo(), cancellationToken);

        return mission;
    }

    public async IAsyncEnumerable<IMission> GetMissions([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (_missions == null)
        {
            var missions = await missionStore.GetMissions(cancellationToken).ToListAsync(cancellationToken);
            _missions = new List<IMission>();
            foreach (var mission in missions)
            {
                _missions.Add(await Rehydrate(mission, CancellationToken.None));
            }
        }

        foreach (var mission in _missions)
        {
            yield return mission;
        }
    }

    public async ValueTask ResumeSuspendedMissions(CancellationToken cancellationToken = default)
    {
        await foreach (var mission in GetMissions(cancellationToken))
        {
            if (mission.Status == MissionStatus.Suspended)
            {
                await mission.Start(cancellationToken);
            }
        };
    }

    public async ValueTask StopRunningMissions(CancellationToken cancellationToken = default)
    {
        var missions = _missions?.Select(m => m.Stop(cancellationToken)).ToList();
        if (missions?.Count > 0)
        {
            await Task.WhenAll(missions.Select(t => t.AsTask()));
        }
    }

    public async ValueTask<IMission?> GetMission(MissionId missionId, CancellationToken cancellationToken = default)
    {
        var missions = await GetMissions(cancellationToken).ToListAsync(cancellationToken: cancellationToken);
        return missions.FirstOrDefault(m => m.Id == missionId);
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

    private void WireUpMission(IMission mission)
    {
        mission.StatusChanged.Subscribe(_ => missionStore.UpdateMission(mission.ToMongo(), CancellationToken.None));
        foreach (var log in logs)
        {
            mission.StatusChanged.Subscribe(status => log.OnStatusChanged(mission, status));
            mission.Event.Subscribe(message => log.OnEvent(mission, message));
            mission.Error.Subscribe(error => log.OnError(mission, error));
        }
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
        var mission = new TradingMission(missionStatus, ship, marketPriorityService, wayfinder, missionScheduler)
        {
            Id = missionId,
            AgentId = ship.AgentId,
        };
        WireUpMission(mission);
        return mission;
    }
}
