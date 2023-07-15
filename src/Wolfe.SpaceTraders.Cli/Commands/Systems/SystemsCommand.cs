using System.CommandLine;

namespace Wolfe.SpaceTraders.Commands.Systems;

internal static class SystemsCommand
{
    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command("systems");
        command.SetHandler(context => services.GetRequiredService<SystemsCommandHandler>().InvokeAsync(context));

        return command;
    }
}