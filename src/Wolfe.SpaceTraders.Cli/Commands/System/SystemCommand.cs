using System.CommandLine;
using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Domain.Models;

namespace Wolfe.SpaceTraders.Cli.Commands.System;

internal static class SystemCommand
{
    public static readonly Argument<SystemSymbol> SystemIdArgument = new("system-id", r => new SystemSymbol(string.Join(' ', r.Tokens.Select(t => t.Value))));

    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command(
            name: "system",
            description: "Displays the details of the given system."
        );
        command.AddArgument(SystemIdArgument);
        command.SetHandler(context => services.GetRequiredService<SystemCommandHandler>().InvokeAsync(context));

        return command;
    }
}