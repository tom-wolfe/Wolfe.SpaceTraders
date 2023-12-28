using Cocona;
using Wolfe.SpaceTraders.Cli.Formatters;
using Wolfe.SpaceTraders.Domain.Systems;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands;

internal class SystemCommands(IExplorationService explorationService)
{
    public async Task<int> Get([Argument] SystemId systemId, CancellationToken cancellationToken = default)
    {
        var system = await explorationService.GetSystem(systemId, cancellationToken) ?? throw new Exception($"System '{systemId}' not found.");
        SystemFormatter.WriteSystem(system);

        return ExitCodes.Success;
    }

    public async Task<int> List(CancellationToken cancellationToken = default)
    {
        var systems = explorationService.GetSystems(cancellationToken);

        await foreach (var system in systems)
        {
            SystemFormatter.WriteSystem(system);
            Console.WriteLine();
        }
        return ExitCodes.Success;
    }

}
