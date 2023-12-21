using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Formatters;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Ships;

internal class ShipsCommandHandler(ISpaceTradersClient client) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var ships = client.GetShips(context.GetCancellationToken());
        await foreach (var ship in ships)
        {
            ShipFormatter.WriteShip(ship);
        }
        return ExitCodes.Success;
    }
}