using System.CommandLine;

namespace Wolfe.SpaceTraders.Cli.Commands.Logout;

internal static class LogoutCommand
{
    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command("logout");
        command.SetHandler(context => services.GetRequiredService<LogoutCommandHandler>().InvokeAsync(context));

        return command;
    }
}