namespace Wolfe.SpaceTraders.Domain.Missions;

/// <summary>
/// Defines a mission that can be executed by a ship.
/// </summary>
public interface IMission
{
    /// <summary>
    /// Gets the unique identifier for the mission.
    /// </summary>
    public MissionId Id { get; }

    /// <summary>
    /// Gets the type of the mission, for example, "Trading" or "Probe".
    /// </summary>
    public MissionType Type { get; }

    /// <summary>
    /// Executes the mission.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to request early termination of the mission.</param>
    /// <returns>A task that resolves when the mission has been completed. Some missions run indefinitely, in which case, <paramref name="cancellationToken"/> can be used to request the mission be terminated.</returns>
    public Task Execute(CancellationToken cancellationToken = default);
}
