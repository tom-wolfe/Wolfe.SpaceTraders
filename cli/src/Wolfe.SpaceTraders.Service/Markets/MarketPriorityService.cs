using System.Runtime.CompilerServices;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.General;
using Wolfe.SpaceTraders.Domain.Marketplaces;

namespace Wolfe.SpaceTraders.Service.Markets;

internal class MarketPriorityService(
    IExplorationService explorationService,
    IMarketplaceService marketplaceService
) : IMarketPriorityService
{
    public async IAsyncEnumerable<MarketPriorityRank> GetPriorityMarkets(WaypointId startingWaypointId, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var start = await explorationService.GetWaypoint(startingWaypointId, cancellationToken) ?? throw new Exception("Invalid starting position.");

        var markets = marketplaceService
            .GetMarketplaces(startingWaypointId.SystemId, cancellationToken)
            .SelectAwaitWithCancellation((m, ct) => Rank(start.Location, m, ct))
            .OrderBy(m => m.Rank);

        await foreach (var market in markets)
        {
            yield return market;
        }
    }

    private async ValueTask<MarketPriorityRank> Rank(Point location, Marketplace marketplace, CancellationToken cancellationToken = default)
    {
        var distanceFromShip = Math.Round(location.DistanceTo(marketplace.Location).Total);

        var marketData = await marketplaceService.GetMarketData(marketplace.Id, cancellationToken);
        var volatility = marketData == null ? 100 : await marketplaceService.GetPercentileVolatility(marketData.Age, cancellationToken);
        var age = marketData == null ? 0 : marketData.Age.TotalMinutes;

        // Adjust priority based on percentile chance of market data having changed.
        var rank = volatility switch
        {
            < 25 => distanceFromShip + age + 9999, // Higher value to discourage frequent updates.
            < 50 => (distanceFromShip + age) * 1.5,
            < 75 => (distanceFromShip + age) * 1.25,
            < 100 => (distanceFromShip + age) * 1.0,
            _ => (distanceFromShip + age) * 0.8 // Lower value to encourage exploration.
        };

        return new MarketPriorityRank(
            marketplace.Id,
            marketplace.Location,
            Math.Round(location.DistanceTo(marketplace.Location).Total),
            marketData?.Age,
            volatility,
            rank
        );
    }
}
