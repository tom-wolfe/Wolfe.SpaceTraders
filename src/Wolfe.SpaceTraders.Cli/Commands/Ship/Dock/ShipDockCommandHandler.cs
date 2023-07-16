using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Ship.Dock;

internal class ShipDockCommandHandler : CommandHandler
{
    private readonly ISpaceTradersClient _client;

    public ShipDockCommandHandler(ISpaceTradersClient client)
    {
        _client = client;
    }

    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var shipId = context.BindingContext.ParseResult.GetValueForArgument(ShipDockCommand.ShipIdArgument);
        await _client.ShipDock(shipId, context.GetCancellationToken());

        Console.WriteLine("Your ship is now docked.".Color(ConsoleColors.Success));
        return ExitCodes.Success;
    }
}