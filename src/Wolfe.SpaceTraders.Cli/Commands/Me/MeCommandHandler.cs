using System.CommandLine.Invocation;

namespace Wolfe.SpaceTraders.Commands.Me;

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

        Console.WriteLine($"ID: {agent.AccountId.Color(ConsoleColors.Id)}");
        Console.WriteLine($"Symbol: {agent.Symbol.Value.Color(ConsoleColors.Code)}");
        Console.WriteLine($"Headquarters: {agent.Headquarters.Color(ConsoleColors.Code)}");
        Console.WriteLine($"Credits: {agent.Credits}");
        Console.WriteLine($"Faction: {agent.StartingFaction.Value.Color(ConsoleColors.Code)}");
        Console.WriteLine($"Ships: {agent.ShipCount}");

        return ExitCodes.Success;
    }
}