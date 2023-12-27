using Humanizer;
using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain.Fleet;

namespace Wolfe.SpaceTraders.Cli.Commands.Extract;

internal class ExtractCommandHandler(IFleetClient fleetClient) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var shipId = context.BindingContext.ParseResult.GetValueForArgument(ExtractCommand.ShipIdArgument);

        var ship = await fleetClient.GetShip(shipId, context.GetCancellationToken())
                  ?? throw new Exception($"Ship {shipId.Value} could not be found.");

        var result = await ship.Extract(context.GetCancellationToken());

        Console.WriteLine($"Successfully extracted {result.Yield.Quantity} {result.Yield.ItemId.Value}.".Color(ConsoleColors.Success));
        Console.WriteLine($"Next extraction possible in {result.Cooldown.Remaining.Humanize()}.".Color(ConsoleColors.Success));
        Console.WriteLine();

        return ExitCodes.Success;
    }
}