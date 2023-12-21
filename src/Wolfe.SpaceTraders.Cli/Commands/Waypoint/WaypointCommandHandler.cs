using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Cli.Formatters;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Waypoint;

internal class WaypointCommandHandler(ISpaceTradersClient client) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var waypointId = context.BindingContext.ParseResult.GetValueForArgument(WaypointCommand.WaypointIdArgument);

        try
        {
            var waypoint = await client.GetWaypoint(waypointId, context.GetCancellationToken())
                ?? throw new Exception($"Waypoint '{waypointId}' not found.");

            WaypointFormatter.WriteWaypoint(waypoint);
            return ExitCodes.Success;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting waypoint: {ex.Message}.".Color(ConsoleColors.Error));
            return ExitCodes.Error;
        }

    }
}