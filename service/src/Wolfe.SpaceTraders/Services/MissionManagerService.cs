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
        var stopTasks = await missionService.GetMissions(CancellationToken.None)
            .Select(m => m.Stop(cancellationToken))
            .ToListAsync(CancellationToken.None);
        await Task.WhenAll(stopTasks);
    }
}
