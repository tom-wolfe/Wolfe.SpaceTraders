using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain.Fleet;

namespace Wolfe.SpaceTraders.Cli.Commands.Refuel;

internal class RefuelCommandHandler(IShipService shipService) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var shipId = context.BindingContext.ParseResult.GetValueForArgument(RefuelCommand.ShipIdArgument);
        var ship = await shipService.GetShip(shipId, context.GetCancellationToken())
                   ?? throw new Exception($"Ship {shipId} could not be found.");

        await ship.Refuel(context.GetCancellationToken());

        Console.WriteLine("Your ship has been refueled.".Color(ConsoleColors.Success));
        Console.WriteLine();

        return ExitCodes.Success;
    }
}