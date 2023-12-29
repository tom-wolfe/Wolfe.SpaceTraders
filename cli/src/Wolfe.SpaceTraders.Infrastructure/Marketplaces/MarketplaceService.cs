using Microsoft.Extensions.Options;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Infrastructure.Data;

namespace Wolfe.SpaceTraders.Infrastructure.Marketplaces;

internal class MarketplaceService(
    IOptions<MarketServiceOptions> options,
    ISpaceTradersDataClient dataClient
) : IMarketplaceService
{
    public Task<double> GetPercentileVolatility(TimeSpan age, CancellationToken cancellationToken = default)
    {
        var range = options.Value.MaxAge - options.Value.MinAge;
        var x = age - options.Value.MinAge;
        var percentile = x.TotalMilliseconds / range.TotalMilliseconds;
        return Task.FromResult(percentile);
    }

    public async Task<MarketData?> GetMarketData(WaypointId marketplaceId, CancellationToken cancellationToken = default)
    {
        return (await dataClient.GetMarketData(marketplaceId, cancellationToken))?.Item;
    }

    public Task AddMarketData(MarketData marketData, CancellationToken cancellationToken = default)
    {
        return dataClient.AddMarketData(marketData, cancellationToken);
    }
}