using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Ship.Orbit;

internal class ShipOrbitCommandHandler(ISpaceTradersClient client) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var shipId = context.BindingContext.ParseResult.GetValueForArgument(ShipOrbitCommand.ShipIdArgument);
        await client.ShipOrbit(shipId, context.GetCancellationToken());

        Console.WriteLine("Your ship is now in orbit.".Color(ConsoleColors.Success));
        Console.WriteLine();

        return ExitCodes.Success;
    }
}