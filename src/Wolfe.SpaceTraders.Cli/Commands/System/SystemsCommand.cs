using System.CommandLine;

namespace Wolfe.SpaceTraders.Commands.System;

internal static class SystemCommand
{
    public static readonly Argument<string> IdArgument = new("id");

    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command("system");
        command.AddArgument(IdArgument);
        command.SetHandler(context => services.GetRequiredService<SystemCommandHandler>().InvokeAsync(context));

        return command;
    }
}