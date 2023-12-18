using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Waypoints;

internal class WaypointsCommandHandler : CommandHandler
{
    private readonly ISpaceTradersClient _client;

    public WaypointsCommandHandler(ISpaceTradersClient client)
    {
        _client = client;
    }

    public override Task<int> InvokeAsync(InvocationContext context)
    {
        var systemId = context.BindingContext.ParseResult.GetValueForArgument(WaypointsCommand.SystemIdArgument);
        var traits = context.BindingContext.ParseResult.GetValueForOption(WaypointsCommand.TraitsOption);
        var type = context.BindingContext.ParseResult.GetValueForOption(WaypointsCommand.TypeOption);
        var waypoints = _client
            .GetWaypoints(systemId, type, traits, context.GetCancellationToken())
            .ToBlockingEnumerable(context.GetCancellationToken())
            .ToList();

        foreach (var waypoint in waypoints)
        {
            Console.WriteLine($"ID: {waypoint.Symbol.Value.Color(ConsoleColors.Id)}");
            Console.WriteLine($"Type: {waypoint.Type.Value.Color(ConsoleColors.Code)}");
            Console.WriteLine($"Position: {waypoint.X}, {waypoint.Y}");
            Console.WriteLine("Traits:");
            foreach (var trait in waypoint.Traits)
            {
                Console.WriteLine($"- {trait.Name.Color(ConsoleColors.Information)} ({trait.Symbol.Value.Color(ConsoleColors.Code)})");
            }

            if (waypoint != waypoints.Last())
            {
                Console.WriteLine();
            }
        }
        return Task.FromResult(ExitCodes.Success);
    }
}