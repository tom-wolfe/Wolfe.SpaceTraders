using System.CommandLine;

namespace Wolfe.SpaceTraders.Commands.Me;

internal static class MeCommand
{
    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command("me");
        command.SetHandler(context => services.GetRequiredService<MeCommandHandler>().InvokeAsync(context));

        return command;
    }
}