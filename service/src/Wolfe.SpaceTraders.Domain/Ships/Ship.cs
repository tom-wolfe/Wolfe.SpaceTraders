using System.Reactive.Linq;
using System.Reactive.Subjects;
using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Ships.Commands;
using Wolfe.SpaceTraders.Domain.Ships.Results;

namespace Wolfe.SpaceTraders.Domain.Ships;

/// <summary>
/// Represents an agent ship in the SpaceTraders universe.
/// </summary>
public class Ship
{
    private readonly Subject<MarketData> _marketDataProbed = new();
    private readonly Subject<WaypointId> _arrived = new();

    private readonly IShipClient _client;
    private ShipNavigation _navigation;
    private readonly ShipFuel _fuel;
    private readonly ShipCargo _cargo;

    private Ship(IShipClient client, ShipNavigation navigation, ShipFuel fuel, ShipCargo cargo)
    {
        _client = client;
        _navigation = navigation;
        _fuel = fuel;
        _cargo = cargo;
    }

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
    /// Gets the ships navigation status.
    /// </summary>
    public IShipNavigation Navigation => _navigation;

    /// <summary>
    /// Gets the ship's fuel status.
    /// </summary>
    public IShipFuel Fuel => _fuel;

    /// <summary>
    /// Gets the details of the ship's cargo storage.
    /// </summary>
    public IShipCargo Cargo => _cargo;

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

        var result = await _client.Navigate(Id, new ShipNavigateCommand { WaypointId = waypointId }, cancellationToken);
        _navigation.Status = ShipNavigationStatus.InTransit;
        _navigation.Destination = result.Destination;
        _fuel.Current = result.FuelRemaining;

        if (Navigation.Destination == null)
        {
            throw new InvalidOperationException("Ship navigation destination is null.");
        }

        Observable
            .Interval(Navigation.Destination.TimeToArrival)
            .Take(1)
            .Select(_ => Observable.FromAsync(OnArrival))
            .Concat()
            .Subscribe();

        return result;
    }

    public async ValueTask Dock(CancellationToken cancellationToken = default)
    {
        if (Navigation.Status == ShipNavigationStatus.InTransit)
        {
            throw new InvalidOperationException("Unable to dock ship while in transit.");
        }

        if (Navigation.Status == ShipNavigationStatus.Docked)
        {
            return;
        }

        await _client.Dock(Id, cancellationToken);
        _navigation.Status = ShipNavigationStatus.Docked;
    }

    public async ValueTask<ShipExtractResult> Extract(CancellationToken cancellationToken = default)
    {
        if (Navigation.Status == ShipNavigationStatus.InTransit)
        {
            throw new InvalidOperationException("Cannot extract resources while in transit.");
        }

        await Dock(cancellationToken);

        var response = await _client.Extract(Id, cancellationToken);
        _cargo.Add(response.Yield);

        return response;
    }

    public async ValueTask Jettison(ItemId itemId, int quantity, CancellationToken cancellationToken = default)
    {
        if (!_cargo.Contains(itemId, quantity))
        {
            throw new InvalidOperationException("Ship does not contain the specified item and quantity.");
        }
        await _client.Jettison(Id, new ShipJettisonCommand { ItemId = itemId, Quantity = quantity }, cancellationToken);
        _cargo.Remove(itemId, quantity);
    }

    public async ValueTask Orbit(CancellationToken cancellationToken = default)
    {
        if (Navigation.Status == ShipNavigationStatus.InTransit)
        {
            throw new InvalidOperationException("Unable to enter orbit while in transit.");
        }

        if (Navigation.Status == ShipNavigationStatus.InOrbit)
        {
            return;
        }

        await _client.Orbit(Id, cancellationToken);
        _navigation.Status = ShipNavigationStatus.InOrbit;
    }

    public async Task<MarketData?> ProbeMarketData(CancellationToken cancellationToken = default)
    {
        if (Navigation.Status == ShipNavigationStatus.InTransit)
        {
            throw new InvalidOperationException("Cannot probe market data while in transit.");
        }

        var result = await _client.ProbeMarketData(Navigation.WaypointId, cancellationToken);
        if (result?.Data != null) { _marketDataProbed.OnNext(result.Data); }

        return result?.Data;
    }

    public async ValueTask Refuel(CancellationToken cancellationToken = default)
    {
        if (Navigation.Status == ShipNavigationStatus.InTransit)
        {
            throw new InvalidOperationException("Unable to refuel while in transit.");
        }

        if (Navigation.Status == ShipNavigationStatus.InOrbit)
        {
            await Dock(cancellationToken);
        }

        var response = await _client.Refuel(Id, cancellationToken);
        _fuel.Current = response.NewValue;
    }

    private async ValueTask SetSpeed(ShipSpeed speed, CancellationToken cancellationToken = default)
    {
        await _client.SetSpeed(Id, speed, cancellationToken);
        _navigation.Speed = speed;
    }

    public async ValueTask<MarketTransaction> Sell(ItemId itemId, int quantity, CancellationToken cancellationToken = default)
    {
        if (!_cargo.Contains(itemId, quantity))
        {
            throw new InvalidOperationException("Ship does not contain the specified item and quantity.");
        }

        var request = new ShipSellCommand
        {
            ItemId = itemId,
            Quantity = quantity,
        };
        var result = await _client.Sell(Id, request, cancellationToken);

        _cargo.Remove(itemId, quantity);

        return result.Transaction;
    }

    private async Task OnArrival(CancellationToken cancellationToken = default)
    {
        var status = await _client.GetNavigationStatus(Id, cancellationToken);
        _navigation.Location = status.Location;
        _navigation.WaypointId = status.WaypointId;
        _navigation.Destination = null;
        _navigation.Status = status.Status;
        _arrived.OnNext(Navigation.WaypointId);
    }

    public static Ship Create(IShipClient client, ShipId shipId, AgentId agentId, string name, ShipRole role, IShipFuelBase fuel, IShipNavigation navigation, IShipCargoBase cargo)
    {
        var ship = new Ship(
            client: client,
            navigation: new ShipNavigation(navigation),
            fuel: new ShipFuel(fuel),
            cargo: new ShipCargo(cargo)
        )
        {
            Id = shipId,
            AgentId = agentId,
            Name = name,
            Role = role,
        };

        return ship;
    }
}
