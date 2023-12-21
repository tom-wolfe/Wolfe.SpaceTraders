using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Waypoints;

internal class WaypointsCommandHandler(ISpaceTradersClient client) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var systemId = context.BindingContext.ParseResult.GetValueForArgument(WaypointsCommand.SystemIdArgument);
        var traits = context.BindingContext.ParseResult.GetValueForOption(WaypointsCommand.TraitsOption);
        var type = context.BindingContext.ParseResult.GetValueForOption(WaypointsCommand.TypeOption);
        var waypoints = client.GetWaypoints(systemId, type, traits!, context.GetCancellationToken());

        var location = context.BindingContext.ParseResult.GetValueForOption(WaypointsCommand.LocationOption);
        Domain.Models.Waypoint? relativeWaypoint = null;
        if (location != null)
        {
            relativeWaypoint = await client.GetWaypoint(location.Value, context.GetCancellationToken())
                ?? throw new Exception("Unable to find relative waypoint.");
            waypoints = waypoints.OrderBy(w => w.Point.DistanceTo(relativeWaypoint.Point).Total);
        }

        await foreach (var waypoint in waypoints)
        {
            Console.WriteLine($"ID: {waypoint.Symbol.Value.Color(ConsoleColors.Id)}");
            Console.WriteLine($"Type: {waypoint.Type.Value.Color(ConsoleColors.Code)}");

            var position = $"Position: {waypoint.Point.ToString().Color(ConsoleColors.Point)}";
            var distance = relativeWaypoint?.Point.DistanceTo(waypoint.Point);
            if (distance != null) { position += $" ({distance.Total.ToString("F").Color(ConsoleColors.Information)})".Color(ConsoleColors.Distance); }
            Console.WriteLine(position);

            Console.WriteLine("Traits:");
            foreach (var trait in waypoint.Traits)
            {
                Console.WriteLine($"- {trait.Name.Color(ConsoleColors.Information)} ({trait.Symbol.Value.Color(ConsoleColors.Code)})");
            }
            Console.WriteLine();
        }
        return ExitCodes.Success;
    }
}