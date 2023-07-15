using System.CommandLine;
using Wolfe.SpaceTraders.Models;

namespace Wolfe.SpaceTraders.Commands.System;

internal static class SystemCommand
{
    public static readonly Argument<SystemSymbol> IdArgument = new("system-id", r => new SystemSymbol(string.Join(' ', r.Tokens.Select(t => t.Value))));

    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command("system");
        command.AddArgument(IdArgument);
        command.SetHandler(context => services.GetRequiredService<SystemCommandHandler>().InvokeAsync(context));

        return command;
    }
}