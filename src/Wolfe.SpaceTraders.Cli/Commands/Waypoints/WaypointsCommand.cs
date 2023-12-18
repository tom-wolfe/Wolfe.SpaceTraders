using System.CommandLine;
using Wolfe.SpaceTraders.Core.Models;

namespace Wolfe.SpaceTraders.Cli.Commands.Waypoints;

internal static class WaypointsCommand
{
    public static readonly Argument<SystemSymbol> SystemIdArgument = new("system-id", r => new SystemSymbol(string.Join(' ', r.Tokens.Select(t => t.Value))));

    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command(
            name: "waypoints",
            description: "Displays the waypoints in the given system."
        );
        command.AddArgument(SystemIdArgument);
        command.SetHandler(context => services.GetRequiredService<WaypointsCommandHandler>().InvokeAsync(context));

        return command;
    }
}