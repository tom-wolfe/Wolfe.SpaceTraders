using System.Runtime.CompilerServices;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.General;

namespace Wolfe.SpaceTraders.Domain.Marketplaces;

internal class MarketPriorityService(
    IExplorationService explorationService,
    IMarketplaceService marketplaceService
) : IMarketPriorityService
{
    public async IAsyncEnumerable<MarketPriorityRank> GetPriorityMarkets(WaypointId startingWaypointId, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var start = await explorationService.GetWaypoint(startingWaypointId, cancellationToken) ?? throw new Exception("Invalid starting position.");
        var allData = await marketplaceService.GetMarketData(startingWaypointId.SystemId, cancellationToken).ToListAsync(cancellationToken);

        var markets = marketplaceService
            .GetMarketplaces(startingWaypointId.SystemId, cancellationToken)
            .Select(m => Rank(start.Location, m, allData))
            .OrderBy(m => m.Rank);

        await foreach (var market in markets)
        {
            yield return market;
        }
    }

    public async Task<MarketTradeRoute?> GetBestTradeRoute(SystemId systemId, CancellationToken cancellationToken)
    {
        var trades = await GetAllTradeRoutes(systemId, cancellationToken);
        var bestTrade = trades
            .OrderByDescending(t => t.SupplyScore)
            .ThenByDescending(t => t.ProfitScore)
            .ThenBy(t => t.Distance)
            .First();
        return bestTrade;
    }

    public async Task<List<MarketTradeRoute>> GetAllTradeRoutes(SystemId systemId, CancellationToken cancellationToken)
    {
        var marketplaces = await marketplaceService.GetMarketplaces(systemId, cancellationToken).ToListAsync(cancellationToken);
        var markets = await marketplaceService.GetMarketData(systemId, cancellationToken).ToListAsync(cancellationToken);

        var tradeRoutes = new List<MarketTradeRoute>();
        foreach (var importMarket in markets)
        {
            foreach (var exportMarket in markets)
            {
                if (importMarket.WaypointId == exportMarket.WaypointId) { continue; }

                var imports = importMarket.TradeGoods.Where(t => t.Type == MarketTradeType.Import).ToList();
                var exports = importMarket.TradeGoods.Where(t => t.Type == MarketTradeType.Export).ToList();

                var matches = imports.SelectMany(import => exports
                    .Where(export => import.ItemId == export.ItemId)
                    .Select(export => new MarketTradeRoute(
                        import.ItemId,
                        marketplaces.First(m => m.Id == importMarket.WaypointId),
                        marketplaces.First(m => m.Id == exportMarket.WaypointId),
                        import,
                        export
                    ))
                );
                tradeRoutes.AddRange(matches);
            }
        }
        return tradeRoutes;
    }

    private MarketPriorityRank Rank(Point location, Marketplace marketplace, IEnumerable<MarketData> allData)
    {
        var distance = location.DistanceTo(marketplace.Location);
        var totalDistance = Math.Round(location.DistanceTo(marketplace.Location).Total);

        var marketData = allData.FirstOrDefault(m => m.WaypointId == marketplace.Id);
        var volatility = marketData == null ? 100 : marketplaceService.GetPercentileVolatility(marketData.Age);
        var age = marketData == null ? 0 : marketData.Age.TotalMinutes;

        // Adjust priority based on percentile chance of market data having changed.
        var rank = volatility switch
        {
            < 25 => totalDistance + age + 9999, // Higher value to discourage frequent updates.
            < 50 => (totalDistance + age) * 1.5,
            < 75 => (totalDistance + age) * 1.25,
            < 100 => (totalDistance + age) * 1.0,
            _ => (totalDistance + age) * 0.8 // Lower value to encourage exploration.
        };

        return new MarketPriorityRank(
            marketplace.Id,
            marketplace.Location,
            distance,
            marketData?.Age,
            volatility,
            rank
        );
    }
}
