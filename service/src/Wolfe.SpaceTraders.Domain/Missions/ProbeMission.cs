using System.Reactive.Linq;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Missions.Scheduling;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Domain.Missions;

/// <summary>
/// A mission that will navigate between marketplaces and probe their market data.
/// </summary>
/// <param name="startingStatus">The status that the mission will start in.</param>
/// <param name="ship">The ship that will navigate and perform the probe.</param>
/// <param name="priorityService">The service that prioritizes market exploration.</param>
/// <param name="scheduler">The object that will be used to handle the running of the mission.</param>
public class ProbeMission(
    MissionStatus startingStatus,
    Ship ship,
    IMarketPriorityService priorityService,
    IMissionScheduler scheduler
) : Mission(startingStatus, ship, scheduler)
{
    /// <inheritdoc/>
    public override MissionType Type => MissionType.Probe;

    /// <inheritdoc/>
    protected override async Task ExecuteCore(CancellationToken cancellationToken = default)
    {
        while (true)
        {
            if (Ship.Navigation.Status == ShipNavigationStatus.InTransit)
            {
                _log.OnNext($"Ship is already in transit. Expected to arrive in {Ship.Navigation.Destination?.TimeToArrival}.");
                await ship.Arrived.Take(1);
            }

            _log.OnNext($"Scanning system for un-probed markets near {Ship.Navigation.WaypointId}...");
            var market = await priorityService.GetPriorityMarkets(Ship.Navigation.WaypointId, cancellationToken).FirstAsync(cancellationToken);
            _log.OnNext($"Setting course for next marketplace: {market.MarketId} at a distance of {market.Distance}.");

            cancellationToken.ThrowIfCancellationRequested();

            if (Ship.Navigation.WaypointId == market.MarketId)
            {
                _log.OnNext($"Ship is already at destination. Collecting market data.");
            }
            else
            {
                var result = await Ship.NavigateTo(market.MarketId, ShipSpeed.Burn, CancellationToken.None) ?? throw new Exception("Probe ship already at destination.");
                _log.OnNext($"Expected to arrive in {result.Destination.TimeToArrival}.");

                await ship.Arrived.Take(1);
                _log.OnNext($"Arrived at destination. Collecting market data.");
            }

            await Ship.ProbeMarketData(CancellationToken.None);
            _log.OnNext($"Data collected.");

            cancellationToken.ThrowIfCancellationRequested();
        }
    }
}