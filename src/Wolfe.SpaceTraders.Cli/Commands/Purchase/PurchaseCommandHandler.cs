using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Requests;

namespace Wolfe.SpaceTraders.Commands.Purchase;

internal class PurchaseCommandHandler : CommandHandler
{
    private readonly ISpaceTradersClient _client;

    public PurchaseCommandHandler(ISpaceTradersClient client)
    {
        _client = client;
    }

    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var waypointId = context.BindingContext.ParseResult.GetValueForArgument(PurchaseCommand.ShipyardIdArgument);
        var shipType = context.BindingContext.ParseResult.GetValueForArgument(PurchaseCommand.ShipTypeArgument);

        try
        {
            var request = new PurchaseShipRequest
            {
                ShipType = shipType,
                WaypointSymbol = waypointId
            };
            var response = await _client.PurchaseShip(request, context.GetCancellationToken());

            Console.WriteLine("Purchased ship successfully".Color(ConsoleColors.Information));

            return ExitCodes.Success;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting shipyard: {ex.Message}.".Color(ConsoleColors.Error));
            return ExitCodes.Error;
        }
    }
}