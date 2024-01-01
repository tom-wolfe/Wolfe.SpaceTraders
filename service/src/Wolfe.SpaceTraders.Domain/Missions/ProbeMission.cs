using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Missions.Logs;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Domain.Missions;

/// <summary>
/// A mission that will navigate between marketplaces and probe their market data.
/// </summary>
/// <remarks>
/// A mission that will navigate between marketplaces and probe their market data.
/// </remarks>
/// <param name="id">A unique identifier for the mission.</param>
/// <param name="log">The log to write entries to.</param>
/// <param name="ship">The ship that will navigate and perform the probe.</param>
/// <param name="marketplaceService">The service that provides market data.</param>
/// <param name="priorityService">The service that prioritizes market exploration.</param>
public class ProbeMission(
    MissionId id,
    IMissionLog log,
    Ship ship,
    IMarketplaceService marketplaceService,
    IMarketPriorityService priorityService
) : Mission(id, MissionType.Probe, log)
{
    /// <inheritdoc/>
    public override async Task Execute(CancellationToken cancellationToken = default)
    {
        try
        {
            while (true)
            {
                log.Write($"Scanning system for un-probed markets near {ship.Navigation.WaypointId}...");
                var market = await priorityService.GetPriorityMarkets(ship.Navigation.WaypointId, cancellationToken).FirstAsync(cancellationToken);
                log.Write($"Setting course for next marketplace: {market.MarketId} at a distance of {market.Distance}.");

                cancellationToken.ThrowIfCancellationRequested();

                if (ship.Navigation.WaypointId == market.MarketId)
                {
                    log.Write($"Ship is already at destination. Collecting market data.");
                }
                else
                {
                    var result = await ship.BeginNavigationTo(market.MarketId, ShipSpeed.Burn, CancellationToken.None)
                                 ?? throw new Exception("Probe ship already at destination.");
                    log.Write($"Expected to arrive in {result.Navigation.Route.TimeToArrival}.");

                    await ship.AwaitArrival(CancellationToken.None);
                    log.Write($"Arrived at destination. Collecting market data.");
                }

                var marketData = await ship.ProbeMarketData(CancellationToken.None) ?? throw new Exception("Missing market data.");
                await marketplaceService.AddMarketData(marketData, CancellationToken.None);
                log.Write($"Data collected.");

                cancellationToken.ThrowIfCancellationRequested();
            }
        }
        catch (OperationCanceledException)
        {
            log.Write($"Cancellation requested. Terminating mission.");
        }

        log.Write($"Mission complete.");
    }


}