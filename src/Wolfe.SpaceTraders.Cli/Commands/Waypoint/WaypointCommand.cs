using System.CommandLine;
using Wolfe.SpaceTraders.Core.Models;

namespace Wolfe.SpaceTraders.Cli.Commands.Waypoint;

internal static class WaypointCommand
{
    public static readonly Argument<WaypointSymbol> WaypointIdArgument = new("waypoint-id", r => new WaypointSymbol(string.Join(' ', r.Tokens.Select(t => t.Value))));

    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command("waypoint");
        command.AddArgument(WaypointIdArgument);
        command.SetHandler(context => services.GetRequiredService<WaypointCommandHandler>().InvokeAsync(context));

        return command;
    }
}