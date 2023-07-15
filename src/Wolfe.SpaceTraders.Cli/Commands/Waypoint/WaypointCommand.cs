using System.CommandLine;

namespace Wolfe.SpaceTraders.Commands.Waypoint;

internal static class WaypointCommand
{
    public static readonly Argument<string> WaypointIdArgument = new("waypoint-id");

    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command("waypoint");
        command.AddArgument(WaypointIdArgument);
        command.SetHandler(context => services.GetRequiredService<WaypointCommandHandler>().InvokeAsync(context));

        return command;
    }
}