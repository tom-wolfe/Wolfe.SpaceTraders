using Humanizer;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Domain.Missions;

/// <summary>
/// A mission that will navigate between marketplaces and probe their market data.
/// </summary>
/// <remarks>
/// A mission that will navigate between marketplaces and probe their market data.
/// </remarks>
/// <param name="log">The log to write entries to.</param>
/// <param name="ship">The ship that will navigate and perform the probe.</param>
/// <param name="marketplaceService">The service that provides market data.</param>
public class ProbeMission(IMissionLog log, Ship ship, IMarketplaceService marketplaceService) : Mission(log)
{
    /// <inheritdoc/>
    public override async Task Execute(CancellationToken cancellationToken = default)
    {
        try
        {
            while (true)
            {
                log.Write("Scanning system for un-probed markets...");
                var marketplace = await HighestPriorityMarketplace(CancellationToken.None);
                var distance = (int)Math.Round(ship.Location.DistanceTo(marketplace.Location).Total);
                log.Write($"Setting course for next marketplace: {marketplace.Id.Value} at a distance of {distance}.");

                cancellationToken.ThrowIfCancellationRequested();

                var result = await ship.BeginNavigationTo(marketplace.Id, ShipSpeed.Burn, CancellationToken.None)
                             ?? throw new Exception("Probe ship already at destination.");
                log.Write($"Expected to arrive in {result.Navigation.Route.TimeToArrival.Humanize()}.");

                await ship.AwaitArrival(CancellationToken.None);
                log.Write("Arrived at destination. Collecting market data.");

                var marketData = await ship.ProbeMarketData(CancellationToken.None) ?? throw new Exception("Missing market data.");
                await marketplaceService.AddMarketData(marketData, CancellationToken.None);
                log.Write("Data collected.");
                log.Write("");

                cancellationToken.ThrowIfCancellationRequested();
            }
        }
        catch (OperationCanceledException)
        {
            log.Write("Cancellation requested. Terminating mission.");
        }

        log.Write("Mission complete.");
    }

    private ValueTask<Marketplace> HighestPriorityMarketplace(CancellationToken cancellationToken = default) => marketplaceService
        .GetMarketplaces(ship.Navigation.WaypointId.SystemId, cancellationToken)
        .Where(m => m.Id != ship.Navigation.WaypointId)
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