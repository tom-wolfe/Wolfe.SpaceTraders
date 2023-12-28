using System.CommandLine;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Cli.Commands.Dock;

internal static class DockCommand
{
    public static readonly Argument<ShipId> ShipIdArgument = new("ship-id", r => new ShipId(string.Join(' ', r.Tokens.Select(t => t.Value))));
    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command(
            name: "dock",
            description: "Dock the given ship at its current location."
        );
        command.AddArgument(ShipIdArgument);
        command.SetHandler(context => services.GetRequiredService<DockCommandHandler>().InvokeAsync(context));

        return command;
    }
}