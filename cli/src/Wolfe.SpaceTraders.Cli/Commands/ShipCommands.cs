using Cocona;
using Humanizer;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Cli.Formatters;
using Wolfe.SpaceTraders.Domain.Marketplace;
using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Domain.Waypoints;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands;

internal class ShipCommands(IShipService shipService)
{
    public async Task<int> Dock([Argument] ShipId shipId, CancellationToken cancellationToken = default)
    {
        var ship = await shipService.GetShip(shipId, cancellationToken) ?? throw new Exception($"Ship {shipId} could not be found.");

        await ship.Dock(cancellationToken);
        Console.WriteLine("Your ship is now docked.".Color(ConsoleColors.Success));

        return ExitCodes.Success;
    }

    public async Task<int> Extract([Argument] ShipId shipId, CancellationToken cancellationToken = default)
    {
        var ship = await shipService.GetShip(shipId, cancellationToken) ?? throw new Exception($"Ship {shipId.Value} could not be found.");

        var result = await ship.Extract(cancellationToken);

        Console.WriteLine($"Successfully extracted {result.Yield.Quantity} {result.Yield.ItemId.Value}.".Color(ConsoleColors.Success));
        Console.WriteLine($"Next extraction possible in {result.Cooldown.Remaining.Humanize()}.".Color(ConsoleColors.Success));
        Console.WriteLine();

        return ExitCodes.Success;
    }

    public async Task<int> Get([Argument] ShipId shipId, CancellationToken cancellationToken = default)
    {
        var ship = await shipService.GetShip(shipId, cancellationToken) ?? throw new Exception($"Ship {shipId} could not be found.");

        ShipFormatter.WriteShip(ship);

        return ExitCodes.Success;
    }

    public async Task<int> List(CancellationToken cancellationToken = default)
    {
        var ships = shipService.GetShips(cancellationToken);
        await foreach (var ship in ships)
        {
            ShipFormatter.WriteShip(ship);
            Console.WriteLine();
        }
        return ExitCodes.Success;
    }

    public async Task<int> Navigate([Argument] ShipId shipId, [Argument] WaypointId waypointId, [Option] FlightSpeed? speed, CancellationToken cancellationToken = default)
    {
        var ship = await shipService.GetShip(shipId, cancellationToken) ?? throw new Exception($"Ship {shipId.Value} could not be found.");

        if (speed != null)
        {
            await ship.SetSpeed(speed.Value, cancellationToken);
            Console.WriteLine($"Engine has been set to {speed.Value.Value.Color(ConsoleColors.Status)} speed.");
        }

        await ship.NavigateTo(waypointId, cancellationToken);

        Console.WriteLine("Your ship is now in transit.".Color(ConsoleColors.Success));
        Console.WriteLine($"Expected to arrive {ship.Navigation.Route.Arrival.Humanize()}.");

        return ExitCodes.Success;
    }

    public async Task<int> Orbit([Argument] ShipId shipId, CancellationToken cancellationToken = default)
    {
        var ship = await shipService.GetShip(shipId, cancellationToken) ?? throw new Exception($"Ship {shipId} could not be found.");

        await ship.Orbit(cancellationToken);
        Console.WriteLine("Your ship is now in orbit.".Color(ConsoleColors.Success));

        return ExitCodes.Success;
    }

    public async Task<int> Sell([Argument] ShipId shipId, [Argument] ItemId itemId, [Argument] int quantity, CancellationToken cancellationToken = default)
    {
        var ship = await shipService.GetShip(shipId, cancellationToken) ?? throw new Exception($"Ship {shipId} could not be found.");

        var transaction = await ship.Sell(itemId, quantity, cancellationToken);

        Console.WriteLine($"Sold {transaction.Quantity} {transaction.ItemId.ToString()!.Color(ConsoleColors.Code)} for {transaction.TotalPrice.ToString().Color(ConsoleColors.Currency)}");
        Console.WriteLine("Sale concluded successfully.".Color(ConsoleColors.Success));

        return ExitCodes.Success;
    }

    public async Task<int> Refuel([Argument] ShipId shipId, CancellationToken cancellationToken = default)
    {
        var ship = await shipService.GetShip(shipId, cancellationToken) ?? throw new Exception($"Ship {shipId} could not be found.");

        await ship.Refuel(cancellationToken);
        Console.WriteLine("Your ship has been refueled.".Color(ConsoleColors.Success));

        return ExitCodes.Success;
    }
}
