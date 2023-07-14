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
        var response = await _client.GetAgent(context.GetCancellationToken());
        var agent = response.Content!.Data;

        Console.WriteLine($"ID: {agent.AccountId}");
        Console.WriteLine($"Symbol: {agent.Symbol}");
        Console.WriteLine($"Headquarters: {agent.Headquarters}");
        Console.WriteLine($"Credits: {agent.Credits}");
        Console.WriteLine($"Faction: {agent.StartingFaction}");
        Console.WriteLine($"Ships: {agent.ShipCount}");

        return ExitCodes.Success;
    }
}