using Humanizer;
using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Cli.Commands.Ship.Extract;

internal class ShipExtractCommandHandler(IShipClient shipClient) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var shipId = context.BindingContext.ParseResult.GetValueForArgument(ShipExtractCommand.ShipIdArgument);
        var result = await shipClient.Extract(shipId, context.GetCancellationToken());

        Console.WriteLine($"Successfully extracted {result.Yield.Quantity} {result.Yield.TradeId.Value}.".Color(ConsoleColors.Success));
        Console.WriteLine($"Next extraction possible in {result.Cooldown.Remaining.Humanize()}.".Color(ConsoleColors.Success));
        Console.WriteLine();

        return ExitCodes.Success;
    }
}