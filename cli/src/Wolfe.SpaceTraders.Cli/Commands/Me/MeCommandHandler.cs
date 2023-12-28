using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Me;

internal class MeCommandHandler(IAgentService agentService) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var agent = await agentService.GetAgent(context.GetCancellationToken());

        Console.WriteLine($"~ {agent.Id.Value.Color(ConsoleColors.Code)}");
        Console.WriteLine($"  Account Id: {agent.AccountId.Value.Color(ConsoleColors.Id)}");
        Console.WriteLine($"  Headquarters: {agent.Headquarters.Value.Color(ConsoleColors.Code)}");
        Console.WriteLine($"  Credits: {agent.Credits.ToString().Color(ConsoleColors.Currency)}");
        Console.WriteLine($"  Faction: {agent.FactionId.Value.Color(ConsoleColors.Code)}");
        Console.WriteLine();

        return ExitCodes.Success;
    }
}