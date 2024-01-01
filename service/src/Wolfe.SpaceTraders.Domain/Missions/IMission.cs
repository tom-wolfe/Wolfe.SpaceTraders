using Wolfe.SpaceTraders.Domain.Ships;

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
    /// Gets the current status of the mission.
    /// </summary>
    public MissionStatus Status { get; }

    /// <summary>
    /// Gets the ID of the ship that is executing the mission.
    /// </summary>
    public ShipId ShipId { get; }

    /// <summary>
    /// Starts the mission.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the mission start.</param>
    /// <returns>A task that will resolve when the mission has started.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the mission is in an incorrect status.</exception>
    public Task Start(CancellationToken cancellationToken = default);

    /// <summary>
    /// Stops the mission.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token that can be used to force-stop the mission.</param>
    /// <returns>A task that will resolve when the mission has stopped.</returns>
    public Task Stop(CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the mission on the current thread.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the mission start.</param>
    /// <returns>A task that will resolve when the mission has completed.</returns>
    public Task Execute(CancellationToken cancellationToken = default);
}
