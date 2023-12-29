using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.General;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Domain.Ships.Commands;
using Wolfe.SpaceTraders.Domain.Ships.Results;


namespace Wolfe.SpaceTraders.Domain.Ships;

public class Ship(
    IShipClient client,
    ShipCargo cargo,
    ShipFuel fuel,
    ShipNavigation navigation
)
{
    public required ShipId Id { get; init; }
    public required string Name { get; init; }
    public required ShipRole Role { get; init; }
    public Point Location => Navigation.Route.Origin.Point;
    public ShipNavigation Navigation { get; private set; } = navigation;
    public ShipFuel Fuel { get; private set; } = fuel;
    public ShipCargo Cargo { get; private set; } = cargo;

    public ValueTask AwaitArrival(CancellationToken cancellationToken = default)
    {
        var tta = Navigation.Route.TimeToArrival;
        if (Navigation.Status != ShipNavigationStatus.InTransit || tta.TotalMilliseconds <= 0)
        {
            return ValueTask.CompletedTask;
        }

        var delay = Task
            .Delay(tta, cancellationToken)
            .ContinueWith(async t => Navigation = await client.GetNavigation(Id, cancellationToken), cancellationToken)
            .Unwrap();
        return new ValueTask(delay);
    }

    public async ValueTask<ShipNavigateResult?> BeginNavigationTo(WaypointId waypointId, ShipSpeed? speed = null, CancellationToken cancellationToken = default)
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

    public async Task<MarketData?> ProbeMarketData(CancellationToken cancellationToken)
    {
        if (Navigation.Status == ShipNavigationStatus.InTransit)
        {
            throw new InvalidOperationException("Cannot probe market data while in transit.");
        }

        var result = await client.ProbeMarketData(Navigation.WaypointId, cancellationToken);
        return result?.Data;
    }

    public async ValueTask Refuel(CancellationToken cancellationToken = default)
    {
        var response = await client.Refuel(Id, cancellationToken);
        Fuel = response.Fuel;
    }

    public async ValueTask SetSpeed(ShipSpeed speed, CancellationToken cancellationToken = default)
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