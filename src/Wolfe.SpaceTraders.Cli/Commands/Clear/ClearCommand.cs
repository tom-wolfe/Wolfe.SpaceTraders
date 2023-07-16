using System.CommandLine;

namespace Wolfe.SpaceTraders.Cli.Commands.Clear;

internal static class ClearCommand
{
    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command("clear");
        command.SetHandler(context => services.GetRequiredService<ClearCommandHandler>().InvokeAsync(context));

        return command;
    }
}