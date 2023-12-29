namespace Wolfe.SpaceTraders.Domain.Missions;

public abstract class Mission : IMission
{
    protected Mission(IMissionLog log)
    {
        Log = log;
    }

    protected IMissionLog Log { get; }

    public abstract Task Execute(CancellationToken cancellationToken = default);
}
