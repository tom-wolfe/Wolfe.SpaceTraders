using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain.Fleet;
using Wolfe.SpaceTraders.Domain.Fleet.Commands;

namespace Wolfe.SpaceTraders.Cli.Commands.Purchase;

internal class PurchaseCommandHandler(IFleetService fleetService) : CommandHandler
{
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
            await fleetService.PurchaseShip(request, context.GetCancellationToken());

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