using System.CommandLine;
using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Cli.Commands.Waypoint;

internal static class WaypointCommand
{
    public static readonly Argument<WaypointId> WaypointIdArgument = new("waypoint-id", r => new WaypointId(string.Join(' ', r.Tokens.Select(t => t.Value))));

    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command(
            name: "waypoint",
            description: "Displays the details of the given waypoint."
        );
        command.AddArgument(WaypointIdArgument);
        command.SetHandler(context => services.GetRequiredService<WaypointCommandHandler>().InvokeAsync(context));

        return command;
    }
}