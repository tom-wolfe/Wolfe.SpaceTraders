using System.CommandLine;
using Wolfe.SpaceTraders.Commands.Contracts.Accept;

namespace Wolfe.SpaceTraders.Commands.Systems.Waypoints;

internal static class WaypointsCommand
{
    public static readonly Argument<string> IdArgument = new("id");
    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command("waypoints");
        command.AddArgument(IdArgument);
        command.SetHandler(context => services.GetRequiredService<AcceptContractCommandHandler>().InvokeAsync(context));

        return command;
    }
}