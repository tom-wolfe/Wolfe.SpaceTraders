using Wolfe.SpaceTraders.Domain.Exploration;

namespace Wolfe.SpaceTraders.Domain.Marketplaces;

public interface IMarketPriorityService
{
    public IAsyncEnumerable<MarketPriorityRank> GetPriorityMarkets(WaypointId startingWaypointId, CancellationToken cancellationToken = default);
}