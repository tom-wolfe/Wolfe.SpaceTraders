using Wolfe.SpaceTraders.Domain.Exploration;

namespace Wolfe.SpaceTraders.Domain.Marketplaces;

/// <summary>
/// Provides functionality for interacting with marketplaces.
/// </summary>
public interface IMarketplaceService
{
    /// <summary>
    /// Gets the marketplace with the specified ID.
    /// </summary>
    /// <param name="marketplaceId">The ID of the market to load.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>The marketplace if it exists, or null if it does not.</returns>
    public Task<Marketplace?> GetMarketplace(WaypointId marketplaceId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all the marketplaces in the given system.
    /// </summary>
    /// <param name="systemId">The ID of the system to load marketplaces for.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>The list of marketplaces.</returns>
    public IAsyncEnumerable<Marketplace> GetMarketplaces(SystemId systemId, CancellationToken cancellationToken = default);

    Task<double> GetPercentileVolatility(TimeSpan age, CancellationToken cancellationToken = default);
    Task<MarketData?> GetMarketData(WaypointId marketplaceId, CancellationToken cancellationToken = default);
    Task AddMarketData(MarketData marketData, CancellationToken cancellationToken = default);
}