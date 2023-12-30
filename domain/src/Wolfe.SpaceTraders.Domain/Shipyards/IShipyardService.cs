using Wolfe.SpaceTraders.Domain.Exploration;

namespace Wolfe.SpaceTraders.Domain.Shipyards;

/// <summary>
/// Provides functionality for interacting with shipyards.
/// </summary>
public interface IShipyardService
{
    /// <summary>
    /// Gets the shipyard with the specified ID.
    /// </summary>
    /// <param name="shipyardId">The ID of the shipyard to load.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>The shipyard if it exists, or null if it does not.</returns>
    public Task<Shipyard?> GetShipyard(WaypointId shipyardId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the shipyards in the given system.
    /// </summary>
    /// <param name="systemId">The system to get shipyards for.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>The discovered shipyards.</returns>
    public IAsyncEnumerable<Shipyard> GetShipyards(SystemId systemId, CancellationToken cancellationToken = default);
}