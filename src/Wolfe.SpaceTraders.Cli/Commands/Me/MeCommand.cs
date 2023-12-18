using System.CommandLine;

namespace Wolfe.SpaceTraders.Cli.Commands.Me;

internal static class MeCommand
{
    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command(
            name: "me",
            description: "Displays information about the current agent."
        );
        command.SetHandler(context => services.GetRequiredService<MeCommandHandler>().InvokeAsync(context));

        return command;
    }
}