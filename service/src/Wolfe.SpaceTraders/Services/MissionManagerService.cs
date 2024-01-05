using Wolfe.SpaceTraders.Domain.Missions;

namespace Wolfe.SpaceTraders.Services;

public class MissionManagerService(IMissionService missionService) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        return missionService.ResumeSuspendedMissions(cancellationToken).AsTask();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return missionService.StopRunningMissions(cancellationToken).AsTask();
    }
}
