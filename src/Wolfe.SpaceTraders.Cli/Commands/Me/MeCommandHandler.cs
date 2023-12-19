using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Me;

internal class MeCommandHandler : CommandHandler
{
    private readonly ISpaceTradersClient _client;

    public MeCommandHandler(ISpaceTradersClient client)
    {
        _client = client;
    }

    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var agent = await _client.GetAgent(context.GetCancellationToken());

        Console.WriteLine($"ID: {agent.AccountId.Value.Color(ConsoleColors.Id)}");
        Console.WriteLine($"Symbol: {agent.Symbol.Value.Color(ConsoleColors.Code)}");
        Console.WriteLine($"Headquarters: {agent.Headquarters.Value.Color(ConsoleColors.Code)}");
        Console.WriteLine($"Credits: {agent.Credits.Currency()}");
        Console.WriteLine($"Faction: {agent.StartingFaction.Value.Color(ConsoleColors.Code)}");

        return ExitCodes.Success;
    }
}