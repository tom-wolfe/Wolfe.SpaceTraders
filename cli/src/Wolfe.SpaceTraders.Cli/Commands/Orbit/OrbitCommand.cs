using System.CommandLine;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Cli.Commands.Orbit;

internal static class OrbitCommand
{
    public static readonly Argument<ShipId> ShipIdArgument = new("ship-id", r => new ShipId(string.Join(' ', r.Tokens.Select(t => t.Value))));
    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command(
            name: "orbit",
            description: "Puts the given ship into orbit at its current location."
        );
        command.AddArgument(ShipIdArgument);
        command.SetHandler(context => services.GetRequiredService<OrbitCommandHandler>().InvokeAsync(context));

        return command;
    }
}