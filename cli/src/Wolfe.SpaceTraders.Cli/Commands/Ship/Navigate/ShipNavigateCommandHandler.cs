﻿using Humanizer;
using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Cli.Commands.Ship.Navigate;

internal class ShipNavigateCommandHandler(IShipClient shipClient) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var shipId = context.BindingContext.ParseResult.GetValueForArgument(ShipNavigateCommand.ShipIdArgument);
        var waypointId = context.BindingContext.ParseResult.GetValueForArgument(ShipNavigateCommand.WaypointIdArgument);
        var speed = context.BindingContext.ParseResult.GetValueForOption(ShipNavigateCommand.SpeedOption);
        if (speed != null)
        {
            await shipClient.SetSpeed(shipId, speed.Value, context.GetCancellationToken());
            Console.WriteLine($"Engine has been set to {speed.Value.Value.Color(ConsoleColors.Status)} speed.");
        }

        var request = new Domain.Ships.Commands.ShipNavigateCommand
        {
            WaypointId = waypointId
        };
        var response = await shipClient.Navigate(shipId, request, context.GetCancellationToken());
        Console.WriteLine("Your ship is now in transit.".Color(ConsoleColors.Success));
        Console.WriteLine($"Expected to arrive {response.Navigation.Route.Arrival.Humanize()}.");
        Console.WriteLine();

        return ExitCodes.Success;
    }
}