using System.Reactive.Linq;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Missions.Scheduling;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Domain.Missions;

/// <summary>
/// A mission that will navigate between marketplaces and probe their market data.
/// </summary>
/// <param name="startingStatus">The status that the mission will start in.</param>
/// <param name="ship">The ship that will navigate and perform the probe.</param>
/// <param name="marketPriority">The service that provides market data.</param>
/// <param name="wayfinder">The service used to provide pathfinding between waypoints.</param>
/// <param name="scheduler">The object that will be used to handle the running of the mission.</param>
public class TradingMission(
    MissionStatus startingStatus,
    Ship ship,
    IMarketPriorityService marketPriority,
    IWayfinderService wayfinder,
    IMissionScheduler scheduler
) : Mission(startingStatus, ship, scheduler)
{
    /// <inheritdoc/>
    public override MissionType Type => MissionType.Trading;

    /// <inheritdoc/>
    protected override async Task ExecuteCore(CancellationToken cancellationToken = default)
    {
        while (true)
        {
            var route = await marketPriority.GetBestTradeRoute(Ship.Navigation.WaypointId.SystemId, cancellationToken)
                        ?? throw new Exception("No trade routes available.");

            cancellationToken.ThrowIfCancellationRequested();

            await NavigateTo(route.ExportDestination.Id, CancellationToken.None);
            await Buy(route.TradeItem, CancellationToken.None);
            await NavigateTo(route.ImportDestination.Id, CancellationToken.None);
            await Sell(route.TradeItem, CancellationToken.None);

            cancellationToken.ThrowIfCancellationRequested();
        }
    }

    private async Task NavigateTo(WaypointId destination, CancellationToken cancellationToken = default)
    {
        var route = await wayfinder.FindPath(Ship.Navigation.WaypointId, destination, Ship.MaximumDistance, cancellationToken);
        foreach (var waypoint in route.Waypoints)
        {
            await Ship.NavigateTo(waypoint, ShipSpeed.Cruise, cancellationToken);
            await Ship.Arrived
                .TakeUntil(_ => cancellationToken.IsCancellationRequested)
                .Take(1);
            await Ship.Refuel(cancellationToken);
        }
    }

    private async Task Buy(ItemId itemId, CancellationToken cancellationToken = default)
    {
        await Ship.ProbeMarketData(cancellationToken);
        //var marketplace = await Ship.
    }

    private async Task Sell(ItemId itemId, CancellationToken cancellationToken = default)
    {
        var count = Ship.Cargo.Items.Where(i => i.Id == itemId).Sum(i => i.Quantity);
        await Ship.Sell(itemId, count, cancellationToken);
    }
}