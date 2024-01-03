using System.Runtime.CompilerServices;
using Wolfe.SpaceTraders.Domain.Missions;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Infrastructure.Missions;

internal class MissionService(IMissionStore missionStore, IMissionFactory missionFactory) : IMissionService
{
    private List<IMission>? _missions;

    public async Task<IMission> CreateProbeMission(Ship ship, CancellationToken cancellationToken = default)
    {
        var missions = await GetMissions(cancellationToken).ToListAsync(cancellationToken);
        if (missions.Any(m => m.ShipId == ship.Id))
        {
            throw new InvalidOperationException("Ship has already been assigned a mission.");
        }

        var mission = missionFactory.CreateProbeMission(ship);

        _missions!.Add(mission);
        await missionStore.UpdateMission(mission, cancellationToken);

        return mission;
    }

    public IEnumerable<IMission> GetRunningMissions()
    {
        return _missions ?? Enumerable.Empty<IMission>();
    }

    public async IAsyncEnumerable<IMission> GetMissions([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        _missions ??= await missionStore.GetMissions(missionFactory, cancellationToken).ToListAsync(cancellationToken);

        foreach (var mission in _missions)
        {
            yield return mission;
        }
    }

    public async ValueTask<IMission?> GetMission(MissionId missionId, CancellationToken cancellationToken = default)
    {
        var missions = await GetMissions(cancellationToken).ToListAsync(cancellationToken: cancellationToken);
        return missions.FirstOrDefault(m => m.Id == missionId);
    }
}
