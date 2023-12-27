﻿using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain.Fleet;

namespace Wolfe.SpaceTraders.Cli.Commands.Ship.Sell;

internal class ShipSellCommandHandler(IFleetClient fleetClient) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var shipId = context.BindingContext.ParseResult.GetValueForArgument(ShipSellCommand.ShipIdArgument);
        var itemId = context.BindingContext.ParseResult.GetValueForArgument(ShipSellCommand.ItemIdArgument);
        var quantity = context.BindingContext.ParseResult.GetValueForArgument(ShipSellCommand.QuantityArgument);

        var ship = await fleetClient.GetShip(shipId, context.GetCancellationToken())
                   ?? throw new Exception($"Ship {shipId} could not be found.");

        var transaction = await ship.Sell(itemId, quantity, context.GetCancellationToken());

        Console.WriteLine($"Sold {transaction.Quantity} {transaction.ItemId.ToString().Color(ConsoleColors.Code)} for {transaction.TotalPrice.ToString().Color(ConsoleColors.Currency)}");
        Console.WriteLine("Sale concluded successfully.".Color(ConsoleColors.Success));

        Console.WriteLine();

        return ExitCodes.Success;
    }
}