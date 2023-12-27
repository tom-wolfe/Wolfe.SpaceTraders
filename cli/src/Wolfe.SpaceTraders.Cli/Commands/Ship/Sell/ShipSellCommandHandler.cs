using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Cli.Commands.Ship.Sell;

internal class ShipSellCommandHandler(IShipClient shipClient) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var shipId = context.BindingContext.ParseResult.GetValueForArgument(ShipSellCommand.ShipIdArgument);
        var itemId = context.BindingContext.ParseResult.GetValueForArgument(ShipSellCommand.ItemIdArgument);
        var quantity = context.BindingContext.ParseResult.GetValueForArgument(ShipSellCommand.QuantityArgument);

        var request = new Domain.Ships.Commands.ShipSellCommand
        {
            ItemId = itemId,
            Quantity = quantity,
        };
        var result = await shipClient.Sell(shipId, request, context.GetCancellationToken());
        var t = result.Transaction;
        Console.WriteLine($"Sold {t.Quantity} {t.TradeId.ToString().Color(ConsoleColors.Code)} for {t.TotalPrice.ToString().Color(ConsoleColors.Currency)}");
        Console.WriteLine("Sale concluded successfully.".Color(ConsoleColors.Success));

        Console.WriteLine();

        return ExitCodes.Success;
    }
}