using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Ship.Sell
{
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
            await client.ShipSell(shipId, request, context.GetCancellationToken());
            Console.WriteLine("Sale concluded successfully.".Color(ConsoleColors.Success));
            Console.WriteLine();

            return ExitCodes.Success;
        }
    }
}

namespace Wolfe.SpaceTraders.Service.Commands
{
}