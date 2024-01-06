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
/// <param name="marketplaceService">The service that provides market data.</param>
/// <param name="scheduler">The object that will be used to handle the running of the mission.</param>
public class TradingMission(
    MissionStatus startingStatus,
    Ship ship,
    IMarketplaceService marketplaceService,
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
            //var route = await GetBestTradeRoute(cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();

            //await NavigateTo(route.ExportDestination, CancellationToken.None);
            //await Buy(route.TradeItem, CancellationToken.None);
            //await NavigateTo(route.ImportDestination, CancellationToken.None);
            //await Sell(route.TradeItem, CancellationToken.None);

            cancellationToken.ThrowIfCancellationRequested();
        }
    }

    private async Task NavigateTo(WaypointId destination, CancellationToken cancellationToken = default)
    {
        var route = await wayfinder.PlotRoute(Ship.Navigation.WaypointId, destination, cancellationToken);
        foreach (var waypoint in route.Waypoints)
        {
            await Ship.BeginNavigationTo(waypoint, ShipSpeed.Cruise, cancellationToken);
            await Ship.AwaitArrival(cancellationToken);
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

    //private async Task<TradeRoute> GetBestTradeRoute(CancellationToken cancellationToken)
    //{
    //    var markets = await marketplaceService.GetMarketData(Ship.Navigation.WaypointId.SystemId, cancellationToken).ToListAsync(cancellationToken);

    //    var tradeRoutes = new List<TradeRoute>();
    //    foreach (var importMarket in markets)
    //    {
    //        foreach (var exportMarket in markets)
    //        {
    //            if (importMarket.WaypointId == exportMarket.WaypointId) { continue; }

    //            importMarket.TradeGoods.ElementAt(0).

    //            var importSupply = importMarket.GetSupply(TradeItem);
    //            var exportSupply = exportMarket.GetSupply(TradeItem);

    //            if (importSupply == null || exportSupply == null)
    //            {
    //                continue;
    //            }

    //            var supply = new MarketTradeSupply(importSupply, exportSupply);

    //            if (supply.Profit > 0)
    //            {
    //                tradeRoutes.Add(new TradeRoute(TradeItem, exportMarket.MarketId, importMarket.MarketId, supply));
    //            }
    //        }
    //    }
    //}

    private record TradeRoute(
        ItemId TradeItem,
        WaypointId ExportDestination,
        WaypointId ImportDestination,
        MarketTradeSupply supply
    );
}