using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain.Fleet;

namespace Wolfe.SpaceTraders.Cli.Commands.Ship.Dock;

internal class ShipDockCommandHandler(IFleetClient fleetClient) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var shipId = context.BindingContext.ParseResult.GetValueForArgument(ShipDockCommand.ShipIdArgument);
        var ship = await fleetClient.GetShip(shipId, context.GetCancellationToken())
            ?? throw new Exception($"Ship {shipId} could not be found.");

        await ship.Dock(context.GetCancellationToken());

        Console.WriteLine("Your ship is now docked.".Color(ConsoleColors.Success));
        Console.WriteLine();

        return ExitCodes.Success;
    }
}