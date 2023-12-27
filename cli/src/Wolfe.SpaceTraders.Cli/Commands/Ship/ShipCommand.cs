using System.CommandLine;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Cli.Commands.Ship;

internal static class ShipCommand
{
    public static readonly Argument<ShipId> ShipIdArgument = new("ship-id", r => new ShipId(string.Join(' ', r.Tokens.Select(t => t.Value))));

    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command(
            name: "ship",
            description: "Displays the details of the given ship."
        );
        command.AddArgument(ShipIdArgument);
        command.SetHandler(context => services.GetRequiredService<ShipCommandHandler>().InvokeAsync(context));

        return command;
    }
}