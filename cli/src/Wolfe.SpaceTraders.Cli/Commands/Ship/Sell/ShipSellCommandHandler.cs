using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Ship.Sell;

internal class ShipSellCommandHandler(ISpaceTradersClient client) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var shipId = context.BindingContext.ParseResult.GetValueForArgument(ShipSellCommand.ShipIdArgument);
        var itemId = context.BindingContext.ParseResult.GetValueForArgument(ShipSellCommand.ItemIdArgument);
        var quantity = context.BindingContext.ParseResult.GetValueForArgument(ShipSellCommand.QuantityArgument);

        var request = new Service.Commands.ShipSellCommand
        {
            ItemId = itemId,
            Quantity = quantity,
        };
        var result = await client.ShipSell(shipId, request, context.GetCancellationToken());
        var t = result.Transaction;
        Console.WriteLine($"Sold {t.Units} {t.TradeSymbol.ToString().Color(ConsoleColors.Code)} for {t.TotalPrice.ToString().Color(ConsoleColors.Currency)}");
        Console.WriteLine("Sale concluded successfully.".Color(ConsoleColors.Success));

        Console.WriteLine();

        return ExitCodes.Success;
    }
}