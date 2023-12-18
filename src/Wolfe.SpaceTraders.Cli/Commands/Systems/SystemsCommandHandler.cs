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

    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var response = await _client.GetSystems(20, 1, context.GetCancellationToken());
        foreach (var system in response.Systems)
        {
            Console.WriteLine($"ID: {system.Symbol.Value.Color(ConsoleColors.Id)}");
            Console.WriteLine($"Type: {system.Type.Value.Color(ConsoleColors.Code)}");
        }
        return ExitCodes.Success;
    }
}