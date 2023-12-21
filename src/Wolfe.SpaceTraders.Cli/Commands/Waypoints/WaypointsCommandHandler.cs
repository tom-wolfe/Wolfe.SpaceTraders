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

    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var systemId = context.BindingContext.ParseResult.GetValueForArgument(WaypointsCommand.SystemIdArgument);
        var traits = context.BindingContext.ParseResult.GetValueForOption(WaypointsCommand.TraitsOption);
        var type = context.BindingContext.ParseResult.GetValueForOption(WaypointsCommand.TypeOption);
        var distance = context.BindingContext.ParseResult.GetValueForOption(WaypointsCommand.DistanceOption);
        var waypoints = _client.GetWaypoints(systemId, type, traits!, context.GetCancellationToken());

        await foreach (var waypoint in waypoints)
        {
            Console.WriteLine($"ID: {waypoint.Symbol.Value.Color(ConsoleColors.Id)}");
            Console.WriteLine($"Type: {waypoint.Type.Value.Color(ConsoleColors.Code)}");

            Console.WriteLine($"Position: {waypoint.Location}");

            // TODO: Restore waypoint traits.
            //Console.WriteLine("Traits:");
            //foreach (var trait in waypoint.Traits)
            //{
            //    Console.WriteLine($"- {trait.Name.Color(ConsoleColors.Information)} ({trait.Symbol.Value.Color(ConsoleColors.Code)})");
            //}
            Console.WriteLine();
        }
        return ExitCodes.Success;
    }
}