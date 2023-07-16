﻿using System.CommandLine;
using Wolfe.SpaceTraders.Core.Models;

namespace Wolfe.SpaceTraders.Cli.Commands.System;

internal static class SystemCommand
{
    public static readonly Argument<SystemSymbol> SystemIdArgument = new("system-id", r => new SystemSymbol(string.Join(' ', r.Tokens.Select(t => t.Value))));

    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command("system");
        command.AddArgument(SystemIdArgument);
        command.SetHandler(context => services.GetRequiredService<SystemCommandHandler>().InvokeAsync(context));

        return command;
    }
}