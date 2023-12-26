using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Formatters;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Waypoints;

internal class WaypointsCommandHandler(ISpaceTradersClient client) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var systemId = context.BindingContext.ParseResult.GetValueForArgument(WaypointsCommand.SystemIdArgument);
        var traits = context.BindingContext.ParseResult.GetValueForOption(WaypointsCommand.TraitsOption) ?? [];
        var type = context.BindingContext.ParseResult.GetValueForOption(WaypointsCommand.TypeOption);
        var waypoints = client.GetWaypoints(systemId, context.GetCancellationToken()).ToBlockingEnumerable(context.GetCancellationToken());

        if (type != null)
        {
            waypoints = waypoints.Where(w => w.Type == type);
        }

        if (traits.Length > 0)
        {
            waypoints = waypoints.Where(w => w.HasAnyTrait(traits));
        }

        var location = context.BindingContext.ParseResult.GetValueForOption(WaypointsCommand.NearestToOption);
        Domain.Waypoints.Waypoint? relativeWaypoint = null;
        if (location != null)
        {
            relativeWaypoint = await client.GetWaypoint(location.Value, context.GetCancellationToken())
                ?? throw new Exception("Unable to find relative waypoint.");
            waypoints = waypoints.OrderBy(w => w.Location.DistanceTo(relativeWaypoint.Location).Total);
        }

        foreach (var waypoint in waypoints)
        {
            WaypointFormatter.WriteWaypoint(waypoint, relativeWaypoint?.Location);
            Console.WriteLine();
        }
        return ExitCodes.Success;
    }
}