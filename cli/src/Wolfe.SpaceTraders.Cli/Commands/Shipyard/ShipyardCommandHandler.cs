using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Cli.Formatters;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Shipyard;

internal class ShipyardCommandHandler(IExplorationService client) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var waypointId = context.BindingContext.ParseResult.GetValueForArgument(ShipyardCommand.ShipyardIdArgument);

        try
        {
            var shipyard = await client.GetShipyard(waypointId, context.GetCancellationToken())
                ?? throw new Exception($"Shipyard '{waypointId}' not found.");

            ShipyardFormatter.WriteShipyard(shipyard);
            Console.WriteLine();

            return ExitCodes.Success;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting shipyard: {ex.Message}.".Color(ConsoleColors.Error));
            return ExitCodes.Error;
        }
    }
}