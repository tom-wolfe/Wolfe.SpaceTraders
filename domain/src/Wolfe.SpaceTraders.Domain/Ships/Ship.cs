using Wolfe.SpaceTraders.Domain.Marketplace;
using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Domain.Ships.Commands;
using Wolfe.SpaceTraders.Domain.Ships.Results;
using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Domain.Ships;

public class Ship(
    IShipClient client,
    ShipCargo cargo,
    ShipFuel fuel,
    Navigation.Navigation navigation
)
{
    public required ShipId Id { get; init; }
    public required string Name { get; init; }
    public required ShipRole Role { get; init; }
    public Navigation.Navigation Navigation { get; private set; } = navigation;
    public ShipFuel Fuel { get; private set; } = fuel;
    public ShipCargo Cargo { get; private set; } = cargo;

    public async Task Dock(CancellationToken cancellationToken = default)
    {
        if (Navigation.Status == NavigationStatus.InTransit)
        {
            throw new InvalidOperationException("Cannot dock ship while in transit.");
        }

        if (Navigation.Status == NavigationStatus.Docked)
        {
            throw new InvalidOperationException("Ship is already docked.");
        }

        var response = await client.Dock(Id, cancellationToken);
        Navigation = response.Navigation;
    }

    public async Task<ShipExtractResult> Extract(CancellationToken cancellationToken = default)
    {
        if (Navigation.Status == NavigationStatus.InTransit)
        {
            throw new InvalidOperationException("Cannot extract resources while in transit.");
        }

        if (Navigation.Status == NavigationStatus.Docked)
        {
            throw new InvalidOperationException("Cannot extract resources while docked.");
        }

        var response = await client.Extract(Id, cancellationToken);
        Cargo = response.Cargo;

        return response;
    }

    public Task Jettison(ItemId itemId, int quantity)
    {
        // TODO: Implement.
        return Task.CompletedTask;
    }

    public async Task NavigateTo(WaypointId waypointId, CancellationToken cancellationToken = default)
    {
        if (Navigation.Status == NavigationStatus.InTransit)
        {
            throw new InvalidOperationException("Cannot navigate when ship is already in transit.");
        }

        if (Navigation.Status == NavigationStatus.Docked)
        {
            throw new InvalidOperationException("Ship is currently docked.");
        }

        var result = await client.Navigate(Id, new ShipNavigateCommand { WaypointId = waypointId }, cancellationToken);
        Navigation = result.Navigation;
        Fuel = result.Fuel;
    }

    public async Task Orbit(CancellationToken cancellationToken = default)
    {
        if (Navigation.Status == NavigationStatus.InTransit)
        {
            throw new InvalidOperationException("Ship is in transit.");
        }

        if (Navigation.Status == NavigationStatus.InOrbit)
        {
            throw new InvalidOperationException("Ship is already in orbit.");
        }

        var result = await client.Orbit(Id, cancellationToken);
        Navigation = result.Navigation;
    }

    public async Task Refuel(CancellationToken cancellationToken = default)
    {
        var response = await client.Refuel(Id, cancellationToken);
        Fuel = response.Fuel;
    }

    public async Task SetSpeed(FlightSpeed speed, CancellationToken cancellationToken = default)
    {
        var response = await client.SetSpeed(Id, speed, cancellationToken);
        Navigation = response.Navigation;
    }

    public async Task<Transaction> Sell(ItemId itemId, int quantity, CancellationToken cancellationToken = default)
    {
        var request = new ShipSellCommand
        {
            ItemId = itemId,
            Quantity = quantity,
        };
        var result = await client.Sell(Id, request, cancellationToken);
        Cargo = result.Cargo;

        return result.Transaction;
    }
}