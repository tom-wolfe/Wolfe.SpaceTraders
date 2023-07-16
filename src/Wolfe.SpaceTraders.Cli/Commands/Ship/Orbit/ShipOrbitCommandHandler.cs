using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Ship.Orbit;

internal class ShipOrbitCommandHandler : CommandHandler
{
    private readonly ISpaceTradersClient _client;

    public ShipOrbitCommandHandler(ISpaceTradersClient client)
    {
        _client = client;
    }

    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var shipId = context.BindingContext.ParseResult.GetValueForArgument(ShipOrbitCommand.ShipIdArgument);
        await _client.ShipOrbit(shipId, context.GetCancellationToken());

        Console.WriteLine("Your ship is now in orbit.".Color(ConsoleColors.Success));
        return ExitCodes.Success;
    }
}