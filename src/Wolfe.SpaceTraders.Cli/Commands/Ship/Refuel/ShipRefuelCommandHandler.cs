using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Ship.Refuel;

internal class ShipRefuelCommandHandler(ISpaceTradersClient client) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var shipId = context.BindingContext.ParseResult.GetValueForArgument(ShipRefuelCommand.ShipIdArgument);
        await client.ShipRefuel(shipId, context.GetCancellationToken());

        Console.WriteLine("Your ship has been refueled.".Color(ConsoleColors.Success));
        Console.WriteLine();

        return ExitCodes.Success;
    }
}