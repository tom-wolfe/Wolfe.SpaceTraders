using Wolfe.SpaceTraders.Domain.Missions;

namespace Wolfe.SpaceTraders.Services;

public class MissionManagerService(IMissionService missionService) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        // TODO: Resume all suspended missions.
        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        var stopTasks = missionService
            .GetRunningMissions()
            .Select(m => m.Stop(cancellationToken).AsTask());
        await Task.WhenAll(stopTasks);
    }
}
