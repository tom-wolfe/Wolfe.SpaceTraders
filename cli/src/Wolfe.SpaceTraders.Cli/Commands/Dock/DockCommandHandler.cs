using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Dock;

internal class DockCommandHandler(IShipService shipService) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var shipId = context.BindingContext.ParseResult.GetValueForArgument(DockCommand.ShipIdArgument);
        var ship = await shipService.GetShip(shipId, context.GetCancellationToken())
            ?? throw new Exception($"Ship {shipId} could not be found.");

        await ship.Dock(context.GetCancellationToken());

        Console.WriteLine("Your ship is now docked.".Color(ConsoleColors.Success));
        Console.WriteLine();

        return ExitCodes.Success;
    }
}