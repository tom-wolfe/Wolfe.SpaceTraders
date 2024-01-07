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
