using Microsoft.Extensions.Options;
using System.Net;
using System.Runtime.CompilerServices;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Infrastructure.Api;
using Wolfe.SpaceTraders.Sdk;

namespace Wolfe.SpaceTraders.Infrastructure.Marketplaces;

internal class MarketplaceService(
    IOptions<MarketplaceServiceOptions> options,
    ISpaceTradersApiClient apiClient,
    IMarketplaceStore marketplaceStore,
    IExplorationService explorationService
) : IMarketplaceService
{
    public async Task<Marketplace?> GetMarketplace(WaypointId marketplaceId, CancellationToken cancellationToken = default)
    {
        var cached = await marketplaceStore.GetMarketplace(marketplaceId, cancellationToken);
        if (cached != null) { return cached; }

        var waypoint = await explorationService.GetWaypoint(marketplaceId, cancellationToken);
        if (waypoint == null) { return null; }
        var response = await apiClient.GetMarketplace(marketplaceId.SystemId.Value, marketplaceId.Value, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
        var market = response.GetContent().Data.ToDomain(waypoint);

        await marketplaceStore.AddMarketplace(market, cancellationToken);
        return market;
    }

    public async IAsyncEnumerable<Marketplace> GetMarketplaces(SystemId systemId, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var cached = marketplaceStore.GetMarketplaces(systemId, cancellationToken);
        var hasCached = false;

        await foreach (var marketplace in cached)
        {
            hasCached = true;
            yield return marketplace;
        }
        if (hasCached) { yield break; }

        var waypoints = explorationService.GetWaypoints(systemId, cancellationToken);
        await foreach (var waypoint in waypoints)
        {
            if (!waypoint.HasMarketplace)
            {
                continue;
            }

            var market = await GetMarketplace(waypoint.Id, cancellationToken);
            if (market == null) { continue; }

            yield return market;
        }
    }

    public Task<double> GetPercentileVolatility(TimeSpan age, CancellationToken cancellationToken = default)
    {
        var range = options.Value.MaxAge - options.Value.MinAge;
        var x = age - options.Value.MinAge;
        var percentile = x.TotalMilliseconds / range.TotalMilliseconds;
        return Task.FromResult(percentile);
    }

    public Task<MarketData?> GetMarketData(WaypointId marketplaceId, CancellationToken cancellationToken = default)
    {
        return marketplaceStore.GetMarketData(marketplaceId, cancellationToken);
    }

    public Task AddMarketData(MarketData marketData, CancellationToken cancellationToken = default)
    {
        return marketplaceStore.AddMarketData(marketData, cancellationToken);
    }
}