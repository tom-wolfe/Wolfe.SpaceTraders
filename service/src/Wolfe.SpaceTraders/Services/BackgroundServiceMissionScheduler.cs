using Wolfe.SpaceTraders.Domain.Missions;
using Wolfe.SpaceTraders.Domain.Missions.Scheduling;

namespace Wolfe.SpaceTraders.Services;

public class BackgroundServiceMissionScheduler : IMissionScheduler
{
    private readonly Dictionary<MissionId, BackgroundServiceMissionRunner> _missionRunners = [];

    public async Task Start(IMission mission, CancellationToken cancellationToken = default)
    {
        if (!_missionRunners.TryGetValue(mission.Id, out var runner))
        {
            runner = new BackgroundServiceMissionRunner(mission);
            _missionRunners.Add(mission.Id, runner);
        }
        await runner.StartAsync(cancellationToken);
    }

    public Task Stop(IMission mission, CancellationToken cancellationToken = default)
    {
        return _missionRunners.TryGetValue(mission.Id, out var runner) ? runner.StopAsync(cancellationToken) : Task.CompletedTask;
    }
}
