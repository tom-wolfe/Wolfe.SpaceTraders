using Microsoft.Extensions.Options;
using System.Net;
using System.Runtime.CompilerServices;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Fleet;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Infrastructure.Api;
using Wolfe.SpaceTraders.Sdk;

namespace Wolfe.SpaceTraders.Infrastructure.Marketplaces;

internal class MarketplaceService : IMarketplaceService
{
    private readonly IOptions<MarketplaceServiceOptions> _options;
    private readonly ISpaceTradersApiClient _apiClient;
    private readonly IMarketplaceStore _marketplaceStore;
    private readonly IExplorationService _explorationService;

    public MarketplaceService(IOptions<MarketplaceServiceOptions> options,
        ISpaceTradersApiClient apiClient,
        IMarketplaceStore marketplaceStore,
        IExplorationService explorationService,
        IFleetService fleetService
    )
    {
        _options = options;
        _apiClient = apiClient;
        _marketplaceStore = marketplaceStore;
        _explorationService = explorationService;

        fleetService.ShipRegistered.Subscribe(OnShipRegistered);
    }

    public async Task<Marketplace?> GetMarketplace(WaypointId marketplaceId, CancellationToken cancellationToken = default)
    {
        var cached = await _marketplaceStore.GetMarketplace(marketplaceId, cancellationToken);
        if (cached != null) { return cached; }

        var waypoint = await _explorationService.GetWaypoint(marketplaceId, cancellationToken);
        if (waypoint == null) { return null; }
        var response = await _apiClient.GetMarketplace(marketplaceId.SystemId.Value, marketplaceId.Value, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
        var market = response.GetContent().Data.ToDomain(waypoint);

        await _marketplaceStore.AddMarketplace(market, cancellationToken);
        return market;
    }

    public async IAsyncEnumerable<Marketplace> GetMarketplaces(SystemId systemId, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var cached = _marketplaceStore.GetMarketplaces(systemId, cancellationToken);
        var hasCached = false;

        await foreach (var marketplace in cached)
        {
            hasCached = true;
            yield return marketplace;
        }
        if (hasCached) { yield break; }

        var waypoints = _explorationService.GetWaypoints(systemId, cancellationToken);
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

    public double GetPercentileVolatility(TimeSpan age)
    {
        var range = _options.Value.MaxAge - _options.Value.MinAge;
        var x = age - _options.Value.MinAge;
        var percentile = x.TotalMilliseconds / range.TotalMilliseconds;
        return percentile;
    }

    public Task<MarketData?> GetMarketData(WaypointId marketplaceId, CancellationToken cancellationToken = default)
    {
        return _marketplaceStore.GetMarketData(marketplaceId, cancellationToken);
    }

    public Task AddMarketData(MarketData marketData, CancellationToken cancellationToken = default)
    {
        return _marketplaceStore.AddMarketData(marketData, cancellationToken);
    }

    public IAsyncEnumerable<MarketData> GetMarketData(SystemId systemId, CancellationToken cancellationToken = default)
    {
        return _marketplaceStore.GetMarketData(systemId, cancellationToken);
    }

    private void OnShipRegistered(Ship ship)
    {
        ship.MarketDataProbed.Subscribe(OnMarketDataProbed);
    }

    private async void OnMarketDataProbed(MarketData data)
    {
        await AddMarketData(data, CancellationToken.None);
    }
}