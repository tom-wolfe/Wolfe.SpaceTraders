using System.CommandLine;
using Wolfe.SpaceTraders.Domain.Systems;

namespace Wolfe.SpaceTraders.Cli.Commands.System;

internal static class SystemCommand
{
    public static readonly Argument<SystemId> SystemIdArgument = new("system-id", r => new SystemId(string.Join(' ', r.Tokens.Select(t => t.Value))));

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