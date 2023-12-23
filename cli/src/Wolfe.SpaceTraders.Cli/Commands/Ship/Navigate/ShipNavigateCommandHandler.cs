using Humanizer;
using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Ship.Navigate;

internal class ShipNavigateCommandHandler(ISpaceTradersClient client) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var shipId = context.BindingContext.ParseResult.GetValueForArgument(ShipNavigateCommand.ShipIdArgument);
        var waypointId = context.BindingContext.ParseResult.GetValueForArgument(ShipNavigateCommand.WaypointIdArgument);
        var speed = context.BindingContext.ParseResult.GetValueForOption(ShipNavigateCommand.SpeedOption);
        if (speed != null)
        {
            await client.SetShipSpeed(shipId, speed.Value, context.GetCancellationToken());
            Console.WriteLine($"Engine has been set to {speed.Value.Value.Color(ConsoleColors.Status)} speed.");
        }

        var request = new Service.Commands.ShipNavigateCommand
        {
            WaypointSymbol = waypointId
        };
        var response = await client.ShipNavigate(shipId, request, context.GetCancellationToken());
        Console.WriteLine("Your ship is now in transit.".Color(ConsoleColors.Success));
        Console.WriteLine($"Expected to arrive {response.Navigation.Route.Arrival.Humanize()}.");
        Console.WriteLine();

        return ExitCodes.Success;
    }
}