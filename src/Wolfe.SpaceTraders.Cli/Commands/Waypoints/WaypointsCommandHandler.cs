using System.CommandLine.Invocation;

namespace Wolfe.SpaceTraders.Commands.Waypoints;

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
        var waypoints = _client
            .GetWaypoints(systemId, context.GetCancellationToken())
            .ToBlockingEnumerable(context.GetCancellationToken())
            .ToList();

        foreach (var waypoint in waypoints)
        {
            Console.WriteLine($"ID: {waypoint.Symbol.Value.Color(ConsoleColors.Id)}");
            Console.WriteLine($"Type: {waypoint.Type.Value.Color(ConsoleColors.Code)}");
            Console.WriteLine($"Position: {waypoint.X}, {waypoint.Y}");

            if (waypoint != waypoints.Last())
            {
                Console.WriteLine();
            }
        }
        return Task.FromResult(ExitCodes.Success);
    }
}