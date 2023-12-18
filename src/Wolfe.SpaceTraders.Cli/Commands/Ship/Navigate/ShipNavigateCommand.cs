using System.CommandLine;
using Wolfe.SpaceTraders.Core.Models;

namespace Wolfe.SpaceTraders.Cli.Commands.Ship.Navigate;

internal static class ShipNavigateCommand
{
    public static readonly Argument<ShipSymbol> ShipIdArgument = new("ship-id", r => new ShipSymbol(string.Join(' ', r.Tokens.Select(t => t.Value))));
    public static readonly Argument<WaypointSymbol> WaypointIdArgument = new("waypoint-id", r => new WaypointSymbol(string.Join(' ', r.Tokens.Select(t => t.Value))));

    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command(
            name: "navigate",
            description: "Begins navigating the given ship to the given waypoint."
        );
        command.AddArgument(ShipIdArgument);
        command.AddArgument(WaypointIdArgument);
        command.SetHandler(context => services.GetRequiredService<ShipNavigateCommandHandler>().InvokeAsync(context));

        return command;
    }
}
