using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Systems;

internal class SystemsCommandHandler : CommandHandler
{
    private readonly ISpaceTradersClient _client;

    public SystemsCommandHandler(ISpaceTradersClient client)
    {
        _client = client;
    }

    public override Task<int> InvokeAsync(InvocationContext context)
    {
        var systems = _client
            .GetSystems(context.GetCancellationToken())
            .ToBlockingEnumerable(context.GetCancellationToken())
            .ToList();

        foreach (var system in systems)
        {
            Console.WriteLine($"ID: {system.Symbol.Value.Color(ConsoleColors.Id)}");
            Console.WriteLine($"Type: {system.Type.Value.Color(ConsoleColors.Code)}");

            if (system != systems.Last())
            {
                Console.WriteLine();
            }
        }
        return Task.FromResult(ExitCodes.Success);
    }
}