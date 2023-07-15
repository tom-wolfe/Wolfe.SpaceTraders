using System.CommandLine;

namespace Wolfe.SpaceTraders.Commands.Waypoints;

internal static class WaypointsCommand
{
    public static readonly Argument<string> SystemIdArgument = new("system-id");

    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command("waypoints");
        command.AddArgument(SystemIdArgument);
        command.SetHandler(context => services.GetRequiredService<WaypointsCommandHandler>().InvokeAsync(context));

        return command;
    }
}