using System.CommandLine;

namespace Wolfe.SpaceTraders.Commands.Ships;

internal static class ShipsCommand
{
    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command("ships");
        command.SetHandler(context => services.GetRequiredService<ShipsCommandHandler>().InvokeAsync(context));

        return command;
    }
}