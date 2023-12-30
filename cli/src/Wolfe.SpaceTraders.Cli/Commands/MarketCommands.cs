using Cocona;
using Microsoft.Extensions.Hosting;
using Wolfe.SpaceTraders.Cli.Formatters;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Marketplaces;

namespace Wolfe.SpaceTraders.Cli.Commands;

internal class MarketCommands(
    IMarketPriorityService marketPriorityService,
    IHostApplicationLifetime host
)
{
    [Command("priority")]
    public async Task<int> Priority([Argument] WaypointId nearestTo)
    {
        var markets = await marketPriorityService.GetPriorityMarkets(nearestTo, host.ApplicationStopping).ToListAsync();

        MarketFormatter.WriteMarketRanks(markets);

        return ExitCodes.Success;
    }
}
