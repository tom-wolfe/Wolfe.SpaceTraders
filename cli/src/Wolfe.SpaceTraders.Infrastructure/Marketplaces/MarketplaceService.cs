using Microsoft.Extensions.Options;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Marketplaces;

namespace Wolfe.SpaceTraders.Infrastructure.Marketplaces;

internal class MarketplaceService(IOptions<MarketServiceOptions> options) : IMarketplaceService
{
    public Task<double> GetPercentileVolatility(TimeSpan age, CancellationToken cancellationToken = default)
    {
        var range = options.Value.MaxAge - options.Value.MinAge;
        var x = age - options.Value.MinAge;
        var percentile = x.TotalMilliseconds / range.TotalMilliseconds;
        return Task.FromResult(percentile);
    }

    public Task<MarketData?> GetMarketData(WaypointId marketplaceId, CancellationToken cancellationToken)
    {
        // TODO: Implement market data retrieval from database.
        throw new NotImplementedException();
    }
}