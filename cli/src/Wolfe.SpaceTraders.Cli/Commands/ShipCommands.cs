using Cocona;
using Microsoft.Extensions.Hosting;
using Wolfe.SpaceTraders.Cli.Formatters;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Service.Ships;

namespace Wolfe.SpaceTraders.Cli.Commands;

internal class ShipCommands(
    IShipService shipService,
    IMarketplaceService marketplaceService,
    IHostApplicationLifetime host
)
{
    public async Task<int> Dock([Argument] ShipId shipId)
    {
        var ship = await shipService.GetShip(shipId, host.ApplicationStopping) ?? throw new Exception($"Ship {shipId} could not be found.");

        await ship.Dock(host.ApplicationStopping);
        ConsoleHelpers.WriteLineSuccess($"Your ship is now docked.");

        return ExitCodes.Success;
    }

    public async Task<int> Extract([Argument] ShipId shipId)
    {
        var ship = await shipService.GetShip(shipId, host.ApplicationStopping) ?? throw new Exception($"Ship {shipId.Value} could not be found.");

        var result = await ship.Extract(host.ApplicationStopping);

        ConsoleHelpers.WriteLineSuccess($"Successfully extracted {result.Yield.Quantity} {result.Yield.ItemId}.");
        ConsoleHelpers.WriteLineFormatted($"Next extraction possible in {result.Cooldown.Remaining}.");

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
            ConsoleHelpers.WriteLineSuccess($"Your ship is already at its destination.");
            return ExitCodes.Success;
        }

        ConsoleHelpers.WriteLineSuccess($"Your ship is now in transit.");
        ConsoleHelpers.WriteLineFormatted($"Expected to arrive {ship.Navigation.Route.Arrival}.");

        if (wait)
        {
            Console.WriteLine("Waiting...");
            await ship.AwaitArrival(host.ApplicationStopping);
            ConsoleHelpers.WriteLineSuccess($"Your ship has arrived.");
        }

        return ExitCodes.Success;
    }

    public async Task<int> Orbit([Argument] ShipId shipId)
    {
        var ship = await shipService.GetShip(shipId, host.ApplicationStopping) ?? throw new Exception($"Ship {shipId} could not be found.");

        await ship.Orbit(host.ApplicationStopping);
        ConsoleHelpers.WriteLineSuccess($"Your ship is now in orbit.");

        return ExitCodes.Success;
    }

    public async Task<int> Probe([Argument] ShipId shipId)
    {
        var ship = await shipService.GetShip(shipId, host.ApplicationStopping) ?? throw new Exception($"Ship {shipId} could not be found.");

        var marketData = await ship.ProbeMarketData(CancellationToken.None) ?? throw new Exception($"There is no market at {ship.Navigation.WaypointId}.");
        await marketplaceService.AddMarketData(marketData, CancellationToken.None);

        MarketFormatter.WriteMarketData(marketData);

        return ExitCodes.Success;
    }

    public async Task<int> Sell([Argument] ShipId shipId, [Argument] ItemId itemId, [Argument] int quantity)
    {
        var ship = await shipService.GetShip(shipId, host.ApplicationStopping) ?? throw new Exception($"Ship {shipId} could not be found.");

        var transaction = await ship.Sell(itemId, quantity, host.ApplicationStopping);

        ConsoleHelpers.WriteLineFormatted($"Sold {transaction.Quantity} {transaction.ItemId} for {transaction.TotalPrice}");
        ConsoleHelpers.WriteLineSuccess($"Sale concluded successfully.");

        return ExitCodes.Success;
    }

    public async Task<int> Refuel([Argument] ShipId shipId)
    {
        var ship = await shipService.GetShip(shipId, host.ApplicationStopping) ?? throw new Exception($"Ship {shipId} could not be found.");

        await ship.Refuel(host.ApplicationStopping);
        ConsoleHelpers.WriteLineSuccess($"Your ship has been refueled.");

        return ExitCodes.Success;
    }

    public async Task<int> Wait([Argument] ShipId shipId)
    {
        var ship = await shipService.GetShip(shipId, host.ApplicationStopping) ?? throw new Exception($"Ship {shipId} could not be found.");

        ConsoleHelpers.WriteLineFormatted($"Waiting for approximately {ship.Navigation.Route.TimeToArrival}...");
        await ship.AwaitArrival(host.ApplicationStopping);
        ConsoleHelpers.WriteLineSuccess($"Your ship has arrived.");

        return ExitCodes.Success;
    }
}
