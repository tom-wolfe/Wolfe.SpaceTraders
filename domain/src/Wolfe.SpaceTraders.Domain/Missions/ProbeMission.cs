using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Domain.Missions;

/// <summary>
/// A mission that will navigate between marketplaces and probe their market data.
/// </summary>
/// <param name="log">The log to write entries to.</param>
/// <param name="ship">The ship that will navigate and perform the probe.</param>
/// <param name="marketplaceService">The service that provides market data.</param>
public class ProbeMission(
    IMissionLog log,
    Ship ship,
    IMarketplaceService marketplaceService
) : Mission(log)
{
    /// <inheritdoc/>
    public override async Task Execute(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var marketplace = await HighestPriorityMarketplace(cancellationToken);
            if (cancellationToken.IsCancellationRequested) { return; }

            await ship.BeginNavigationTo(marketplace.Id, ShipSpeed.Cruise, cancellationToken);
            await ship.AwaitArrival(cancellationToken);
            var marketData = await ship.ProbeMarketData(cancellationToken) ?? throw new Exception("Missing market data.");
            await marketplaceService.AddMarketData(marketData, cancellationToken);
        }
    }

    private ValueTask<Marketplace> HighestPriorityMarketplace(CancellationToken cancellationToken = default) => marketplaceService
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