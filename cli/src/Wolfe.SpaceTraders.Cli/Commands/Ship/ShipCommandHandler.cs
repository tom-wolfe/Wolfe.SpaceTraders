﻿using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Cli.Formatters;
using Wolfe.SpaceTraders.Domain.Fleet;

namespace Wolfe.SpaceTraders.Cli.Commands.Ship;

internal class ShipCommandHandler(IFleetClient fleetClient) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var shipId = context.BindingContext.ParseResult.GetValueForArgument(ShipCommand.ShipIdArgument);
        try
        {
            var ship = await fleetClient.GetShip(shipId, context.GetCancellationToken())
                       ?? throw new Exception($"Ship '{shipId}' not found.");
            ShipFormatter.WriteShip(ship);
            Console.WriteLine();

            return ExitCodes.Success;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting ship: {ex.Message}.".Color(ConsoleColors.Error));
            return ExitCodes.Error;
        }
    }
}