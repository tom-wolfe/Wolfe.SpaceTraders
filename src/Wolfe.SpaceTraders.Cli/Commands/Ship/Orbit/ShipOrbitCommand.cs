using System.CommandLine;
using Wolfe.SpaceTraders.Core.Models;

namespace Wolfe.SpaceTraders.Cli.Commands.Ship.Orbit;

internal static class ShipOrbitCommand
{
    public static readonly Argument<ShipSymbol> ShipIdArgument = new("ship-id", r => new ShipSymbol(string.Join(' ', r.Tokens.Select(t => t.Value))));
    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command("orbit");
        command.AddArgument(ShipIdArgument);
        command.SetHandler(context => services.GetRequiredService<ShipOrbitCommandHandler>().InvokeAsync(context));

        return command;
    }
}