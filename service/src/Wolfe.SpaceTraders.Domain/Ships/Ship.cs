using System.Reactive.Linq;
using System.Reactive.Subjects;
using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.General;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Ships.Commands;
using Wolfe.SpaceTraders.Domain.Ships.Results;

namespace Wolfe.SpaceTraders.Domain.Ships;

/// <summary>
/// Represents an agent ship in the SpaceTraders universe.
/// </summary>
/// <param name="client">The client that provides the ship behavior.</param>
/// <param name="cargo">The cargo contained inside the ship.</param>
/// <param name="fuel">The current fuel capacity of the ship.</param>
/// <param name="navigation">The ships navigation parameters.</param>
public class Ship(
    IShipClient client,
    ShipCargo cargo,
    ShipFuel fuel,
    ShipNavigation navigation
)
{
    private readonly Subject<MarketData> _marketDataProbed = new();
    private readonly Subject<WaypointId> _arrived = new();

    /// <summary>
    /// Gets the unique identifier of the ship.
    /// </summary>
    public required ShipId Id { get; init; }

    /// <summary>
    /// Gets the identifier of the agent who owns the ship.
    /// </summary>
    public required AgentId AgentId { get; init; }

    /// <summary>
    /// Gets the name of the ship (usually the same as the ID).
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Gets the publicly registered role of the ship.
    /// </summary>
    public required ShipRole Role { get; init; }

    /// <summary>
    /// Gets the current location of the ship.
    /// </summary>
    public Point Location => Navigation.Route.Origin.Point;

    /// <summary>
    /// Gets the ships navigation status.
    /// </summary>
    public ShipNavigation Navigation { get; private set; } = navigation;

    /// <summary>
    /// Gets the ship's fuel status.
    /// </summary>
    public ShipFuel Fuel { get; private set; } = fuel;

    /// <summary>
    /// Gets the details of the ship's cargo storage.
    /// </summary>
    public ShipCargo Cargo { get; private set; } = cargo;

    /// <summary>
    /// An observable that will emit a value whenever the ship arrives at a destination waypoint.
    /// </summary>
    public IObservable<WaypointId> Arrived => _arrived.AsObservable();

    /// <summary>
    /// An observable that will emit a value whenever the ship probes market data.
    /// </summary>
    public IObservable<MarketData> MarketDataProbed => _marketDataProbed.AsObservable();

    /// <summary>
    /// Sets the ship to start navigating to the specified waypoint at the given speed. If no speed is specified, the previous speed will be used.
    /// </summary>
    /// <param name="waypointId">The ID of the waypoint to travel to.</param>
    /// <param name="speed">The speed at which to travel. If no speed is specified, the current speed will be used.s</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>The result of the navigation, or null if the ship is already at its destination.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async ValueTask<ShipNavigateResult?> NavigateTo(WaypointId waypointId, ShipSpeed? speed = null, CancellationToken cancellationToken = default)
    {
        if (Navigation.Status == ShipNavigationStatus.InTransit)
        {
            throw new InvalidOperationException("Cannot navigate when ship is already in transit.");
        }

        if (Navigation.WaypointId == waypointId)
        {
            // Already at destination.
            return null;
        }

        await Orbit(cancellationToken);

        if (speed != null) { await SetSpeed(speed.Value, cancellationToken); }

        var result = await client.Navigate(Id, new ShipNavigateCommand { WaypointId = waypointId }, cancellationToken);
        Navigation = result.Navigation;
        Fuel = result.Fuel;

        Observable
            .Interval(result.Navigation.Route.TimeToArrival)
            .Take(1)
            .Select(_ => Observable.FromAsync(async () =>
            {
                Navigation = await client.GetNavigation(Id, cancellationToken);
                _arrived.OnNext(Navigation.WaypointId);
            }))
            .Concat()
            .Subscribe();

        return result;
    }

    public async ValueTask Dock(CancellationToken cancellationToken = default)
    {
        if (Navigation.Status == ShipNavigationStatus.InTransit)
        {
            throw new InvalidOperationException("Cannot dock ship while in transit.");
        }

        if (Navigation.Status == ShipNavigationStatus.Docked)
        {
            return;
        }

        var response = await client.Dock(Id, cancellationToken);
        Navigation = response.Navigation;
    }

    public async ValueTask<ShipExtractResult> Extract(CancellationToken cancellationToken = default)
    {
        if (Navigation.Status == ShipNavigationStatus.InTransit)
        {
            throw new InvalidOperationException("Cannot extract resources while in transit.");
        }

        await Dock(cancellationToken);

        var response = await client.Extract(Id, cancellationToken);
        Cargo = response.Cargo;

        return response;
    }

    public async ValueTask Jettison(ItemId itemId, int quantity, CancellationToken cancellationToken = default)
    {
        var result = await client.Jettison(Id, new ShipJettisonCommand { ItemId = itemId, Quantity = quantity }, cancellationToken);
        Cargo = result.Cargo;
    }

    public async ValueTask Orbit(CancellationToken cancellationToken = default)
    {
        if (Navigation.Status == ShipNavigationStatus.InTransit)
        {
            throw new InvalidOperationException("Ship is in transit.");
        }

        if (Navigation.Status == ShipNavigationStatus.InOrbit)
        {
            return;
        }

        var result = await client.Orbit(Id, cancellationToken);
        Navigation = result.Navigation;
    }

    public async Task<MarketData?> ProbeMarketData(CancellationToken cancellationToken = default)
    {
        if (Navigation.Status == ShipNavigationStatus.InTransit)
        {
            throw new InvalidOperationException("Cannot probe market data while in transit.");
        }

        var result = await client.ProbeMarketData(Navigation.WaypointId, cancellationToken);
        if (result?.Data != null) { _marketDataProbed.OnNext(result.Data); }

        return result?.Data;
    }

    public async ValueTask Refuel(CancellationToken cancellationToken = default)
    {
        var response = await client.Refuel(Id, cancellationToken);
        Fuel = response.Fuel;
    }

    private async ValueTask SetSpeed(ShipSpeed speed, CancellationToken cancellationToken = default)
    {
        var response = await client.SetSpeed(Id, speed, cancellationToken);
        Navigation = response.Navigation;
    }

    public async ValueTask<MarketTransaction> Sell(ItemId itemId, int quantity, CancellationToken cancellationToken = default)
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