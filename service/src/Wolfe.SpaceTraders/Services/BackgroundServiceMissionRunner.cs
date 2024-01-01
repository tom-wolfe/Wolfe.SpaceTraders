using Wolfe.SpaceTraders.Domain.Missions;

namespace Wolfe.SpaceTraders.Services;

public class BackgroundServiceMissionRunner(IMission mission) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken) => mission.Execute(stoppingToken);
}
