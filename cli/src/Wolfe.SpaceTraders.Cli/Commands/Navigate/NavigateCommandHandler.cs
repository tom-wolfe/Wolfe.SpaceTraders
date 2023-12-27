using Humanizer;
using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain.Fleet;

namespace Wolfe.SpaceTraders.Cli.Commands.Navigate;

internal class NavigateCommandHandler(IShipService shipService) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var shipId = context.BindingContext.ParseResult.GetValueForArgument(NavigateCommand.ShipIdArgument);
        var waypointId = context.BindingContext.ParseResult.GetValueForArgument(NavigateCommand.WaypointIdArgument);

        var ship = await shipService.GetShip(shipId, context.GetCancellationToken())
            ?? throw new Exception($"Ship {shipId.Value} could not be found.");

        var speed = context.BindingContext.ParseResult.GetValueForOption(NavigateCommand.SpeedOption);
        if (speed != null)
        {
            await ship.SetSpeed(speed.Value, context.GetCancellationToken());
            Console.WriteLine($"Engine has been set to {speed.Value.Value.Color(ConsoleColors.Status)} speed.");
        }

        await ship.NavigateTo(waypointId, context.GetCancellationToken());

        Console.WriteLine("Your ship is now in transit.".Color(ConsoleColors.Success));
        Console.WriteLine($"Expected to arrive {ship.Navigation.Route.Arrival.Humanize()}.");
        Console.WriteLine();

        return ExitCodes.Success;
    }
}