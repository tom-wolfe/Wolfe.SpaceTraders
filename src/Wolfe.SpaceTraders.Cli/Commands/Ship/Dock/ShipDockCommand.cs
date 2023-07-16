﻿using System.CommandLine;
using Wolfe.SpaceTraders.Core.Models;

namespace Wolfe.SpaceTraders.Cli.Commands.Ship.Dock;

internal static class ShipDockCommand
{
    public static readonly Argument<ShipSymbol> ShipIdArgument = new("ship-id", r => new ShipSymbol(string.Join(' ', r.Tokens.Select(t => t.Value))));
    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command("dock");
        command.AddArgument(ShipIdArgument);
        command.SetHandler(context => services.GetRequiredService<ShipDockCommandHandler>().InvokeAsync(context));

        return command;
    }
}