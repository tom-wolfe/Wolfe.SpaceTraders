using System.CommandLine.Invocation;

namespace Wolfe.SpaceTraders.Commands.Ships;

internal class ShipsCommandHandler : CommandHandler
{
    private readonly ISpaceTradersClient _client;

    public ShipsCommandHandler(ISpaceTradersClient client)
    {
        _client = client;
    }

    public override Task<int> InvokeAsync(InvocationContext context)
    {
        var ships = _client
            .GetShips(context.GetCancellationToken())
            .ToBlockingEnumerable(context.GetCancellationToken())
            .ToList();

        foreach (var ship in ships)
        {
            Console.WriteLine($"ID: {ship.Symbol.Value.Color(ConsoleColors.Id)}");

            if (ship != ships.Last())
            {
                Console.WriteLine();
            }
        }
        return Task.FromResult(ExitCodes.Success);
    }
}