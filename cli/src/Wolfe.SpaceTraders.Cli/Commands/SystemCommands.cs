using Cocona;
using Microsoft.Extensions.Hosting;
using Wolfe.SpaceTraders.Cli.Formatters;
using Wolfe.SpaceTraders.Domain.Exploration;

namespace Wolfe.SpaceTraders.Cli.Commands;

internal class SystemCommands(IExplorationService explorationService, IHostApplicationLifetime host)
{
    public async Task<int> Get([Argument] SystemId systemId)
    {
        var system = await explorationService.GetSystem(systemId, host.ApplicationStopping) ?? throw new Exception($"System '{systemId}' not found.");
        SystemFormatter.WriteSystem(system);

        return ExitCodes.Success;
    }

    public async Task<int> List()
    {
        var systems = explorationService.GetSystems(host.ApplicationStopping);

        await foreach (var system in systems)
        {
            SystemFormatter.WriteSystem(system);
            Console.WriteLine();
        }
        return ExitCodes.Success;
    }

}
