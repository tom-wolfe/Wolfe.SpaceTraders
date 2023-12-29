using Wolfe.SpaceTraders.Domain.Exploration;

namespace Wolfe.SpaceTraders.Domain.Marketplaces;

public interface IMarketplaceService
{
    Task<decimal> GetPercentileVolatility(TimeSpan timeSinceLastUpdate, CancellationToken cancellationToken = default);
    Task<MarketData?> GetMarketData(WaypointId marketplaceId, CancellationToken cancellationToken);
}

public class MarketData
{
    public DateTimeOffset RetrievedAt { get; init; }
    public TimeSpan Age => DateTimeOffset.UtcNow - RetrievedAt;
}