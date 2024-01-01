using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Domain.Missions;

public class TradingMission(MissionId id, IMissionLog log, Ship ship, IWayfinderService wayfinderService) : Mission(id, MissionType.Trading, log)
{
    public override async Task Execute(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var route = await GetBestTradeRoute();
            await NavigateTo(route.ExportDestination);
            await Buy(route.TradeItem);
            await NavigateTo(route.ImportDestination);
            await Sell(route.TradeItem);
        }
    }

    private ValueTask<TradeRoute> GetBestTradeRoute()
    {
        throw new NotImplementedException();
    }

    private async ValueTask NavigateTo(WaypointId destination)
    {
        var route = wayfinderService.PlotRoute(ship, destination);
        await foreach (var stop in route)
        {
            await ship.BeginNavigationTo(stop.Waypoint);
            if (stop.Refuel)
            {
                await ship.Refuel();
            }
        }
    }

    private ValueTask Buy(ItemId item)
    {
        throw new NotImplementedException();
    }

    private ValueTask Sell(ItemId item)
    {
        throw new NotImplementedException();
    }

    private record TradeRoute(ShipId ShipId, WaypointId ExportDestination, WaypointId ImportDestination, ItemId TradeItem);
}
