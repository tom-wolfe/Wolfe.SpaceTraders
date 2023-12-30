using Cocona;
using Humanizer;
using Microsoft.Extensions.Hosting;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Cli.Formatters;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Service.Ships;

namespace Wolfe.SpaceTraders.Cli.Commands;

internal class ShipCommands(IShipService shipService, IHostApplicationLifetime host)
{
    public async Task<int> Dock([Argument] ShipId shipId)
    {
        var ship = await shipService.GetShip(shipId, host.ApplicationStopping) ?? throw new Exception($"Ship {shipId} could not be found.");

        await ship.Dock(host.ApplicationStopping);
        Console.WriteLine("Your ship is now docked.".Color(ConsoleColors.Success));

        return ExitCodes.Success;
    }

    public async Task<int> Extract([Argument] ShipId shipId)
    {
        var ship = await shipService.GetShip(shipId, host.ApplicationStopping) ?? throw new Exception($"Ship {shipId.Value} could not be found.");

        var result = await ship.Extract(host.ApplicationStopping);

        Console.WriteLine($"Successfully extracted {result.Yield.Quantity} {result.Yield.ItemId.Value}.".Color(ConsoleColors.Success));
        Console.WriteLine($"Next extraction possible in {result.Cooldown.Remaining.Humanize()}.".Color(ConsoleColors.Success));
        Console.WriteLine();

        return ExitCodes.Success;
    }

    public async Task<int> Get([Argument] ShipId shipId)
    {
        var ship = await shipService.GetShip(shipId, host.ApplicationStopping) ?? throw new Exception($"Ship {shipId} could not be found.");

        ShipFormatter.WriteShip(ship);

        return ExitCodes.Success;
    }

    public async Task<int> List()
    {
        var ships = shipService.GetShips(host.ApplicationStopping);
        await foreach (var ship in ships)
        {
            ShipFormatter.WriteShip(ship);
            Console.WriteLine();
        }
        return ExitCodes.Success;
    }

    public async Task<int> Navigate([Argument] ShipId shipId, [Argument] WaypointId waypointId, [Option] ShipSpeed? speed, [Option] bool wait)
    {
        var ship = await shipService.GetShip(shipId, host.ApplicationStopping) ?? throw new Exception($"Ship {shipId.Value} could not be found.");

        var result = await ship.BeginNavigationTo(waypointId, speed, host.ApplicationStopping);

        if (result == null)
        {
            Console.WriteLine("Your ship is already at its destination.".Color(ConsoleColors.Success));
            return ExitCodes.Success;
        }

        Console.WriteLine("Your ship is now in transit.".Color(ConsoleColors.Success));
        Console.WriteLine($"Expected to arrive {ship.Navigation.Route.Arrival.Humanize()}.");

        if (wait)
        {
            Console.WriteLine("Waiting...");
            await ship.AwaitArrival(host.ApplicationStopping);
            Console.WriteLine("Your ship has arrived.".Color(ConsoleColors.Success));
        }

        return ExitCodes.Success;
    }

    public async Task<int> Orbit([Argument] ShipId shipId)
    {
        var ship = await shipService.GetShip(shipId, host.ApplicationStopping) ?? throw new Exception($"Ship {shipId} could not be found.");

        await ship.Orbit(host.ApplicationStopping);
        Console.WriteLine("Your ship is now in orbit.".Color(ConsoleColors.Success));

        return ExitCodes.Success;
    }

    public async Task<int> Sell([Argument] ShipId shipId, [Argument] ItemId itemId, [Argument] int quantity)
    {
        var ship = await shipService.GetShip(shipId, host.ApplicationStopping) ?? throw new Exception($"Ship {shipId} could not be found.");

        var transaction = await ship.Sell(itemId, quantity, host.ApplicationStopping);

        Console.WriteLine($"Sold {transaction.Quantity} {transaction.ItemId.ToString()!.Color(ConsoleColors.Code)} for {transaction.TotalPrice.ToString().Color(ConsoleColors.Currency)}");
        Console.WriteLine("Sale concluded successfully.".Color(ConsoleColors.Success));

        return ExitCodes.Success;
    }

    public async Task<int> Refuel([Argument] ShipId shipId)
    {
        var ship = await shipService.GetShip(shipId, host.ApplicationStopping) ?? throw new Exception($"Ship {shipId} could not be found.");

        await ship.Refuel(host.ApplicationStopping);
        Console.WriteLine("Your ship has been refueled.".Color(ConsoleColors.Success));

        return ExitCodes.Success;
    }

    public async Task<int> Wait([Argument] ShipId shipId)
    {
        var ship = await shipService.GetShip(shipId, host.ApplicationStopping) ?? throw new Exception($"Ship {shipId} could not be found.");

        await ship.AwaitArrival(host.ApplicationStopping);
        Console.WriteLine("Your ship has arrived.".Color(ConsoleColors.Success));

        return ExitCodes.Success;
    }
}
