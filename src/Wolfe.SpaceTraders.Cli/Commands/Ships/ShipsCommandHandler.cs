using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Ships;

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
            Console.WriteLine($"- {ship.Symbol.Value.Color(ConsoleColors.Id)} ({ship.Registration.Role.Value.Color(ConsoleColors.Category)}) [{ship.Navigation.Status.Value.Color(ConsoleColors.Status)}]");
            Console.WriteLine($"  Location: {ship.Navigation.WaypointSymbol.Value.Color(ConsoleColors.Code)}");
            // TODO: Restore ship fuel.
            if (!ship.Fuel.IsEmpty)
            {
                var percent = (int)Math.Round(ship.Fuel.Current / ship.Fuel.Capacity * 100m);
                Console.WriteLine($"  Fuel: {ship.Fuel.Current}/{ship.Fuel.Capacity} ({percent}%)");
            }

            if (ship != ships.Last())
            {
                Console.WriteLine();
            }
        }
        return Task.FromResult(ExitCodes.Success);
    }
}