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

    /// <summary>
    /// Gets the percentage chance that the market data will have changed in the given time period.
    /// </summary>
    /// <param name="age">The time between market data refreshes.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>A number between 0 and 100 indicating the percentage chance that the market data will have changed.</returns>
    Task<double> GetPercentileVolatility(TimeSpan age, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all the market data in the given system.
    /// </summary>
    /// <param name="systemId">The ID of the system to get market data for.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>The probed market data in the system.</returns>
    IAsyncEnumerable<MarketData> GetMarketData(SystemId systemId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the market data for the given marketplace.
    /// </summary>
    /// <param name="marketplaceId">The ID of the market to get data for.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>The data for the marketplace if there is any, otherwise null.</returns>
    Task<MarketData?> GetMarketData(WaypointId marketplaceId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds the given market data to the data store.
    /// </summary>
    /// <param name="marketData">The market data to store.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>A task that resolves when the operation has completed.</returns>
    Task AddMarketData(MarketData marketData, CancellationToken cancellationToken = default);
}