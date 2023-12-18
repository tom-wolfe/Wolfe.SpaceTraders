using System.CommandLine;

namespace Wolfe.SpaceTraders.Cli.Commands.Systems;

internal static class SystemsCommand
{
    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command(
            name: "systems",
            description: "Lists the star systems in the universe."
        );
        command.SetHandler(context => services.GetRequiredService<SystemsCommandHandler>().InvokeAsync(context));

        return command;
    }
}