using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Infrastructure.Data;

namespace Wolfe.SpaceTraders.Infrastructure.Marketplaces;

internal interface IMarketplaceStore
{
    public Task AddMarketData(MarketData marketData, CancellationToken cancellationToken);
    public Task AddMarketplace(Marketplace marketplace, CancellationToken cancellationToken = default);
    public Task<DataItemResponse<MarketData>?> GetMarketData(WaypointId marketplaceId, CancellationToken cancellationToken);
    public Task<Marketplace?> GetMarketplace(WaypointId marketplaceId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<Marketplace> GetMarketplaces(SystemId systemId, CancellationToken cancellationToken = default);
}