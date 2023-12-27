using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Cli.Commands.Ship.Orbit;

internal class ShipOrbitCommandHandler(IShipClient shipClient) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var shipId = context.BindingContext.ParseResult.GetValueForArgument(ShipOrbitCommand.ShipIdArgument);
        await shipClient.Orbit(shipId, context.GetCancellationToken());

        Console.WriteLine("Your ship is now in orbit.".Color(ConsoleColors.Success));
        Console.WriteLine();

        return ExitCodes.Success;
    }
}