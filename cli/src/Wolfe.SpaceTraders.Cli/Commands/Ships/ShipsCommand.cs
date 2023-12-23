using System.CommandLine;

namespace Wolfe.SpaceTraders.Cli.Commands.Ships;

internal static class ShipsCommand
{
    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command(
            name: "ships",
            description: "Displays the ships owned by the current player."
        );
        command.SetHandler(context => services.GetRequiredService<ShipsCommandHandler>().InvokeAsync(context));

        return command;
    }
}