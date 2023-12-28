using Wolfe.SpaceTraders.Domain.Marketplace;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Domain.Missions;

public class TradingMission(Ship ship)
{
    public async Task Execute(CancellationToken cancellationToken = default)
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

    private ValueTask NavigateTo(WaypointId destination)
    {
        throw new NotImplementedException();
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
