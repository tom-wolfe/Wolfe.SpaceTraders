using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Cli.Commands.Ship.Dock;

internal class ShipDockCommandHandler(IShipClient shipClient) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var shipId = context.BindingContext.ParseResult.GetValueForArgument(ShipDockCommand.ShipIdArgument);
        await shipClient.Dock(shipId, context.GetCancellationToken());

        Console.WriteLine("Your ship is now docked.".Color(ConsoleColors.Success));
        Console.WriteLine();


        return ExitCodes.Success;
    }
}