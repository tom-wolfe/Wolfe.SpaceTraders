using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Cli.Commands.Ship.Refuel;

internal class ShipRefuelCommandHandler(IShipClient shipClient) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var shipId = context.BindingContext.ParseResult.GetValueForArgument(ShipRefuelCommand.ShipIdArgument);
        await shipClient.Refuel(shipId, context.GetCancellationToken());

        Console.WriteLine("Your ship has been refueled.".Color(ConsoleColors.Success));
        Console.WriteLine();

        return ExitCodes.Success;
    }
}