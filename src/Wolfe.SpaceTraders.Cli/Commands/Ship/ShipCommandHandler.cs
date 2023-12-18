using Humanizer;
using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Core.Models;
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
            Console.WriteLine($"Status: {ship.Nav.Status.Value.Color(ConsoleColors.Code)}");
            if (ship.Nav.Status == ShipNavStatus.InTransit)
            {
                Console.WriteLine($"Arrival: {ship.Nav.Route.Arrival.Humanize().Color(ConsoleColors.Information)}");
            }

            Console.WriteLine($"Engine: {ship.Engine.Symbol.Value.Color(ConsoleColors.Information)}");
            Console.WriteLine($"Frame: {ship.Frame.Symbol.Value.Color(ConsoleColors.Information)}");

            Console.WriteLine("Modules".Heading());
            foreach (var module in ship.Modules)
            {
                Console.WriteLine($"- {module.Symbol.Value.Color(ConsoleColors.Information)}");
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