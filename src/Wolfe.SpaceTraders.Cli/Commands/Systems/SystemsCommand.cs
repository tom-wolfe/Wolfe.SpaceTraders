using System.CommandLine;
using Wolfe.SpaceTraders.Commands.Systems.Waypoints;

namespace Wolfe.SpaceTraders.Commands.Systems;

internal static class SystemsCommand
{
    public static readonly Argument<string?> IdArgument = new("id");

    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command("systems");
        command.AddArgument(IdArgument);
        command.SetHandler(context => services.GetRequiredService<SystemsCommandHandler>().InvokeAsync(context));

        command.AddCommand(WaypointsCommand.CreateCommand(services));

        return command;
    }
}