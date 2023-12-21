using Humanizer;
using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Ship.Extract;

internal class ShipExtractCommandHandler(ISpaceTradersClient client) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var shipId = context.BindingContext.ParseResult.GetValueForArgument(ShipExtractCommand.ShipIdArgument);
        var result = await client.ShipExtract(shipId, context.GetCancellationToken());

        Console.WriteLine($"Successfully extracted {result.Yield.Units} {result.Yield.Symbol.Value}.".Color(ConsoleColors.Success));
        Console.WriteLine($"Next extraction possible in {result.Cooldown.Remaining.Humanize()}.".Color(ConsoleColors.Success));

        return ExitCodes.Success;
    }
}