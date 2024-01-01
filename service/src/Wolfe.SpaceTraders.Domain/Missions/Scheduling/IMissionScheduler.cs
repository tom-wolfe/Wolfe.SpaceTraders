namespace Wolfe.SpaceTraders.Domain.Missions.Scheduling;

public interface IMissionScheduler
{
    public Task Start(IMission mission, CancellationToken cancellationToken = default);
    public Task Stop(IMission mission, CancellationToken cancellationToken = default);
}
