using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Domain.Missions;

public class ProbeMission(
    IMissionLog log,
    Ship ship,
    IExplorationService explorationService,
    IMarketplaceService marketplaceService
) : Mission(log)
{
    public override async Task Execute(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var marketplace = await HighestPriorityMarketplace(cancellationToken);
            if (cancellationToken.IsCancellationRequested) { return; }

            await ship.BeginNavigationTo(marketplace.Id, FlightSpeed.Cruise, cancellationToken);
            await ship.AwaitArrival(cancellationToken);
            var marketData = await ship.ProbeMarketData(cancellationToken) ?? throw new Exception("Missing market data.");
            await marketplaceService.AddMarketData(marketData, cancellationToken);
        }
    }

    private ValueTask<Marketplace> HighestPriorityMarketplace(CancellationToken cancellationToken = default) => explorationService
        .GetMarketplaces(ship.Navigation.WaypointId.SystemId, cancellationToken)
        .OrderByAwaitWithCancellation(MarketplacePriority)
        .FirstAsync(cancellationToken);

    private async ValueTask<double> MarketplacePriority(Marketplace marketplace, CancellationToken cancellationToken = default)
    {
        var distanceFromShip = Math.Round(ship.Location.DistanceTo(marketplace.Location).Total);

        var marketData = await marketplaceService.GetMarketData(marketplace.Id, cancellationToken);
        var volatility = marketData == null ? 100 : await marketplaceService.GetPercentileVolatility(marketData.Age, cancellationToken);

        // Adjust priority based on percentile chance of market data having changed.
        return volatility switch
        {
            < 25 => distanceFromShip * 0.5, // Lower value to discourage frequent updates.
            < 50 => distanceFromShip * 1.0,
            < 75 => distanceFromShip * 1.1,
            < 100 => distanceFromShip * 1.2,
            _ => distanceFromShip * 1.5 // Slightly boosted value to encourage exploration.
        };
    }
}