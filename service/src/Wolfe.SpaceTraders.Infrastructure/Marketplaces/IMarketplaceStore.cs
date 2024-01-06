using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Marketplaces;

namespace Wolfe.SpaceTraders.Infrastructure.Marketplaces;

public interface IMarketplaceStore
{
    public Task AddMarketData(MarketData marketData, CancellationToken cancellationToken = default);
    public Task AddMarketplace(Marketplace marketplace, CancellationToken cancellationToken = default);
    public Task<MarketData?> GetMarketData(WaypointId marketplaceId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<MarketData> GetMarketData(SystemId systemId, CancellationToken cancellationToken = default);
    public Task<Marketplace?> GetMarketplace(WaypointId marketplaceId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<Marketplace> GetMarketplaces(SystemId systemId, CancellationToken cancellationToken = default);
    public Task Clear(CancellationToken cancellationToken = default);
}