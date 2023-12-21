using Humanizer;
using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Ship;

internal class ShipCommandHandler : CommandHandler
{
    private readonly ISpaceTradersClient _client;

    public ShipCommandHandler(ISpaceTradersClient client)
    {
        _client = client;
    }

    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var id = context.BindingContext.ParseResult.GetValueForArgument(ShipCommand.ShipIdArgument);
        try
        {
            var ship = await _client.GetShip(id, context.GetCancellationToken());
            if (ship == null)
            {
                throw new Exception($"Ship '{id}' not found.");
            }

            Console.WriteLine($"ID: {ship.Symbol.Value.Color(ConsoleColors.Id)}");
            Console.WriteLine($"Status: {ship.Navigation.Status.Value.Color(ConsoleColors.Status)}");
            if (ship.Navigation.Status == NavigationStatus.InTransit)
            {
                Console.WriteLine($"Arrival: {ship.Navigation.Route.Arrival.Humanize().Color(ConsoleColors.Information)}");
            }

            return ExitCodes.Success;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting ship: {ex.Message}.".Color(ConsoleColors.Error));
            return ExitCodes.Error;
        }
    }
}