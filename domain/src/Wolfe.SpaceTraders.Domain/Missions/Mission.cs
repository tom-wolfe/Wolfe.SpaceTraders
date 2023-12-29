namespace Wolfe.SpaceTraders.Domain.Missions;

/// <summary>
/// Provides some basic functionality for a ship mission.
/// </summary>
public abstract class Mission : IMission
{
    /// <summary>
    /// Creates a new instance of <see cref="Mission"/>.
    /// </summary>
    /// <param name="log">An object that can be used to track the progress of the mission.</param>
    protected Mission(IMissionLog log)
    {
        Log = log;
    }

    /// <summary>
    /// Gets the mission log.
    /// </summary>
    protected IMissionLog Log { get; }

    /// <inheritdoc/>
    public abstract Task Execute(CancellationToken cancellationToken = default);
}
