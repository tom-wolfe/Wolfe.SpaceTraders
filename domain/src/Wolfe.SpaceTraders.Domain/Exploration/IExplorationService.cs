namespace Wolfe.SpaceTraders.Domain.Exploration;

/// <summary>
/// Provides functionality for exploring the SpaceTraders universe.
/// </summary>
public interface IExplorationService
{
    /// <summary>
    /// Gets the system with the specified ID.
    /// </summary>
    /// <param name="systemId">The ID of the system to get.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>The found system if it exists, otherwise null.</returns>
    public Task<StarSystem?> GetSystem(SystemId systemId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all the systems in the universe.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>The found systems.</returns>
    public IAsyncEnumerable<StarSystem> GetSystems(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the waypoint with the specified ID.
    /// </summary>
    /// <param name="waypointId">The ID of the waypoint to get.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>The found waypoint if it exists, otherwise null.</returns>
    public Task<Waypoint?> GetWaypoint(WaypointId waypointId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all the waypoints in a given system.
    /// </summary>
    /// <param name="systemId">The ID of the system to get waypoints for.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>The found waypoints.</returns>
    public IAsyncEnumerable<Waypoint> GetWaypoints(SystemId systemId, CancellationToken cancellationToken = default);
}