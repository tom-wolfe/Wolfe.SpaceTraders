using Cocona;
using Microsoft.Extensions.Hosting;
using Wolfe.SpaceTraders.Cli.Formatters;
using Wolfe.SpaceTraders.Infrastructure.Exploration;
using Wolfe.SpaceTraders.Infrastructure.Marketplaces;
using Wolfe.SpaceTraders.Infrastructure.Shipyards;

namespace Wolfe.SpaceTraders.Cli.Commands;

internal class DatabaseCommands(
    IExplorationStore explorationStore,
    IMarketplaceStore marketplaceStore,
    IShipyardStore shipyardStore,
    IHostApplicationLifetime host
)
{
    [Command("reset")]
    public async Task<int> Reset()
    {
        await Task.WhenAll(
            explorationStore.Clear(host.ApplicationStopping),
            marketplaceStore.Clear(host.ApplicationStopping),
            shipyardStore.Clear(host.ApplicationStopping)
        );
        ConsoleHelpers.WriteLineSuccess($"Database cleared");

        return ExitCodes.Success;
    }
}
