using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Missions.Scheduling;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Domain.Missions;

/// <summary>
/// A mission that will navigate between marketplaces and probe their market data.
/// </summary>
/// <param name="startingStatus">The status that the mission will start in.</param>
/// <param name="log">The log to write entries to.</param>
/// <param name="ship">The ship that will navigate and perform the probe.</param>
/// <param name="marketplaceService">The service that provides market data.</param>
/// <param name="priorityService">The service that prioritizes market exploration.</param>
/// <param name="scheduler">The object that will be used to handle the running of the mission.</param>
public class ProbeMission(
    MissionStatus startingStatus,
    IMissionLog log,
    Ship ship,
    IMarketplaceService marketplaceService,
    IMarketPriorityService priorityService,
    IMissionScheduler scheduler
) : Mission(startingStatus, ship, log, scheduler)
{
    /// <inheritdoc/>
    public override MissionType Type => MissionType.Probe;

    /// <inheritdoc/>
    protected override async Task ExecuteCore(CancellationToken cancellationToken = default)
    {
        while (true)
        {
            if (ship.Navigation.Status == ShipNavigationStatus.InTransit)
            {
                await Log.Write($"Ship is already in transit. Expected to arrive in {ship.Navigation.Route.TimeToArrival}.", cancellationToken);
                await ship.AwaitArrival(cancellationToken);
            }

            await Log.Write($"Scanning system for un-probed markets near {Ship.Navigation.WaypointId}...", cancellationToken);
            var market = await priorityService.GetPriorityMarkets(Ship.Navigation.WaypointId, cancellationToken).FirstAsync(cancellationToken);
            await Log.Write($"Setting course for next marketplace: {market.MarketId} at a distance of {market.Distance}.", cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();

            if (Ship.Navigation.WaypointId == market.MarketId)
            {
                await Log.Write($"Ship is already at destination. Collecting market data.", cancellationToken);
            }
            else
            {
                var result = await Ship.BeginNavigationTo(market.MarketId, ShipSpeed.Burn, CancellationToken.None)
                             ?? throw new Exception("Probe ship already at destination.");
                await Log.Write($"Expected to arrive in {result.Navigation.Route.TimeToArrival}.", cancellationToken);

                await Ship.AwaitArrival(cancellationToken);
                await Log.Write($"Arrived at destination. Collecting market data.", cancellationToken);
            }

            var marketData = await Ship.ProbeMarketData(CancellationToken.None) ?? throw new Exception("Missing market data.");
            await marketplaceService.AddMarketData(marketData, CancellationToken.None);
            await Log.Write($"Data collected.", cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();
        }
    }
}