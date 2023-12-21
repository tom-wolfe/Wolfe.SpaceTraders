using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Waypoint;

internal class WaypointCommandHandler : CommandHandler
{
    private readonly ISpaceTradersClient _client;

    public WaypointCommandHandler(ISpaceTradersClient client)
    {
        _client = client;
    }

    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var waypointId = context.BindingContext.ParseResult.GetValueForArgument(WaypointCommand.WaypointIdArgument);

        try
        {
            var waypoint = await _client.GetWaypoint(waypointId, context.GetCancellationToken());
            if (waypoint == null)
            {
                throw new Exception($"Waypoint '{waypointId}' not found.");
            }

            Console.WriteLine($"ID: {waypoint.Symbol.Value.Color(ConsoleColors.Id)}");
            Console.WriteLine($"Type: {waypoint.Type.Value.Color(ConsoleColors.Code)}");
            Console.WriteLine($"Position: {waypoint.Location}");
            // TODO: List orbitals and factions

            return ExitCodes.Success;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting waypoint: {ex.Message}.".Color(ConsoleColors.Error));
            return ExitCodes.Error;
        }

    }
}