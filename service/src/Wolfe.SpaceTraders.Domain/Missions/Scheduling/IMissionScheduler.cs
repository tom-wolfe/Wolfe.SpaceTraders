namespace Wolfe.SpaceTraders.Domain.Missions.Scheduling;

public interface IMissionScheduler
{
    public ValueTask Start(IMission mission, CancellationToken cancellationToken = default);
    public ValueTask Stop(IMission mission, CancellationToken cancellationToken = default);
}
