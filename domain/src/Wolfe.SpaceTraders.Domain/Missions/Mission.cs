namespace Wolfe.SpaceTraders.Domain.Missions;

/// <summary>
/// Provides some basic functionality for a ship mission.
/// </summary>
public abstract class Mission : IMission
{
    /// <summary>
    /// Creates a new instance of <see cref="Mission"/>.
    /// </summary>
    /// <param name="id">A unique identifier for the mission.</param>
    /// <param name="type">The type of mission.</param>
    /// <param name="log">An object that can be used to track the progress of the mission.</param>
    protected Mission(MissionId id, MissionType type, IMissionLog log)
    {
        Id = id;
        Type = type;
        Log = log;
    }

    /// <inheritdoc/>
    public MissionId Id { get; }

    /// <inheritdoc/>
    public MissionType Type { get; }

    /// <summary>
    /// Gets the mission log.
    /// </summary>
    protected IMissionLog Log { get; }

    /// <inheritdoc/>
    public abstract Task Execute(CancellationToken cancellationToken = default);
}
