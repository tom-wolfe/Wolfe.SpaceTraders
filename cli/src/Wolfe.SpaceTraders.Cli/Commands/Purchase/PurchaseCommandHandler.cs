using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain.Fleet;
using Wolfe.SpaceTraders.Service.Commands;

namespace Wolfe.SpaceTraders.Cli.Commands.Purchase;

internal class PurchaseCommandHandler : CommandHandler
{
    private readonly IShipService _client;

    public PurchaseCommandHandler(IShipService client)
    {
        _client = client;
    }

    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var waypointId = context.BindingContext.ParseResult.GetValueForArgument(PurchaseCommand.ShipyardIdArgument);
        var shipType = context.BindingContext.ParseResult.GetValueForArgument(PurchaseCommand.ShipTypeArgument);

        try
        {
            var request = new PurchaseShipCommand
            {
                ShipType = shipType,
                WaypointId = waypointId
            };
            await _client.PurchaseShip(request, context.GetCancellationToken());

            Console.WriteLine("Purchased ship successfully".Color(ConsoleColors.Success));

            return ExitCodes.Success;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting shipyard: {ex.Message}".Color(ConsoleColors.Error));
            return ExitCodes.Error;
        }
    }
}