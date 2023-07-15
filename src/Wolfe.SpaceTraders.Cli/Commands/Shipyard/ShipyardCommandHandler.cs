using System.CommandLine.Invocation;

namespace Wolfe.SpaceTraders.Commands.Shipyard;

internal class ShipyardCommandHandler : CommandHandler
{
    private readonly ISpaceTradersClient _client;

    public ShipyardCommandHandler(ISpaceTradersClient client)
    {
        _client = client;
    }

    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var waypointId = context.BindingContext.ParseResult.GetValueForArgument(ShipyardCommand.ShipyardIdArgument);

        try
        {
            var shipyard = await _client.GetShipyard(waypointId, context.GetCancellationToken());
            if (shipyard == null)
            {
                throw new Exception($"Shipyard '{waypointId}' not found.");
            }

            Console.WriteLine("=== SHIPYARD ===");

            foreach (var ship in shipyard.ShipTypes)
            {
                Console.WriteLine($"- {ship.Type.Value.Color(ConsoleColors.Code)}");
            }
            return ExitCodes.Success;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting shipyard: {ex.Message}.".Color(ConsoleColors.Error));
            return ExitCodes.Error;
        }
    }
}