﻿using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain.Fleet;

namespace Wolfe.SpaceTraders.Cli.Commands.Ship.Orbit;

internal class ShipOrbitCommandHandler(IFleetClient fleetClient) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var shipId = context.BindingContext.ParseResult.GetValueForArgument(ShipOrbitCommand.ShipIdArgument);

        var ship = await fleetClient.GetShip(shipId, context.GetCancellationToken())
            ?? throw new Exception($"Ship {shipId} could not be found.");

        await ship.Orbit(context.GetCancellationToken());

        Console.WriteLine("Your ship is now in orbit.".Color(ConsoleColors.Success));
        Console.WriteLine();

        return ExitCodes.Success;
    }
}