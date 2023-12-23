using System.CommandLine;
using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Domain.Models.Ships;

namespace Wolfe.SpaceTraders.Cli.Commands.Ship.Orbit;

internal static class ShipOrbitCommand
{
    public static readonly Argument<ShipSymbol> ShipIdArgument = new("ship-id", r => new ShipSymbol(string.Join(' ', r.Tokens.Select(t => t.Value))));
    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command(
            name: "orbit",
            description: "Puts the given ship into orbit at its current location."
        );
        command.AddArgument(ShipIdArgument);
        command.SetHandler(context => services.GetRequiredService<ShipOrbitCommandHandler>().InvokeAsync(context));

        return command;
    }
}