using Wolfe.SpaceTraders.Domain.Exploration;

namespace Wolfe.SpaceTraders.Domain.Marketplaces;

public interface IMarketPriorityService
{
    public Task<MarketTradeRoute?> GetBestTradeRoute(SystemId systemId, CancellationToken cancellationToken = default);
    public Task<List<MarketTradeRoute>> GetAllTradeRoutes(SystemId systemId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<MarketPriorityRank> GetPriorityMarkets(WaypointId startingWaypointId, CancellationToken cancellationToken = default);
}