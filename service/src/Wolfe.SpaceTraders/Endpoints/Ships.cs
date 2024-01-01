using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Fleet;
using Wolfe.SpaceTraders.Domain.Fleet.Commands;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Endpoints;

public static class Ships
{
    public static WebApplication MapShipEndpoints(this WebApplication app)
    {
        var shipsGroup = app.MapGroup("/ships");

        shipsGroup.MapGet("/", (IFleetService fleetService, CancellationToken cancellationToken = default) => fleetService.GetShips(cancellationToken));
        shipsGroup.MapPost("/", (IFleetService fleetService, PurchaseShipCommand command, CancellationToken cancellationToken = default) => fleetService.PurchaseShip(command, cancellationToken));

        var shipGroup = shipsGroup.MapGroup("/{shipId}");
        shipGroup.MapGet("/", async (IFleetService fleetService, ShipId shipId, CancellationToken cancellationToken = default) =>
        {
            var ship = await fleetService.GetShip(shipId, cancellationToken);
            return ship == null ? Results.NotFound() : Results.Ok(ship);
        });
        shipGroup.MapPost("/dock", async (IFleetService fleetService, ShipId shipId, CancellationToken cancellationToken = default) =>
        {
            var ship = await fleetService.GetShip(shipId, cancellationToken);
            if (ship == null) { return Results.NotFound(); }

            await ship.Dock(cancellationToken);
            return Results.Ok(ship);
        });
        shipGroup.MapPost("/extract", async (IFleetService fleetService, ShipId shipId, CancellationToken cancellationToken = default) =>
        {
            var ship = await fleetService.GetShip(shipId, cancellationToken);
            if (ship == null) { return Results.NotFound(); }

            var result = await ship.Extract(cancellationToken);
            return Results.Ok(result);
        });
        shipGroup.MapPost("/navigate", async (IFleetService fleetService, ShipId shipId, ShipNavigateRequest request, CancellationToken cancellationToken = default) =>
        {
            var ship = await fleetService.GetShip(shipId, cancellationToken);
            if (ship == null) { return Results.NotFound(); }

            var result = await ship.BeginNavigationTo(request.WaypointId, request.Speed, cancellationToken);
            if (request.Wait == true)
            {
                await ship.AwaitArrival(cancellationToken);
            }
            return Results.Ok(result);
        });
        shipGroup.MapPost("/orbit", async (IFleetService fleetService, ShipId shipId, CancellationToken cancellationToken = default) =>
        {
            var ship = await fleetService.GetShip(shipId, cancellationToken);
            if (ship == null) { return Results.NotFound(); }

            await ship.Orbit(cancellationToken);
            return Results.Ok(ship);
        });
        shipGroup.MapPost("/probe", async (IFleetService fleetService, ShipId shipId, CancellationToken cancellationToken = default) =>
        {
            var ship = await fleetService.GetShip(shipId, cancellationToken);
            if (ship == null) { return Results.NotFound(); }

            var result = await ship.ProbeMarketData(cancellationToken);
            return result == null ? Results.NotFound() : Results.Ok(result);
        });
        shipGroup.MapPost("/refuel", async (IFleetService fleetService, ShipId shipId, CancellationToken cancellationToken = default) =>
        {
            var ship = await fleetService.GetShip(shipId, cancellationToken);
            if (ship == null) { return Results.NotFound(); }

            await ship.Refuel(cancellationToken);
            return Results.Ok(ship);
        });
        shipGroup.MapPost("/sell", async (IFleetService fleetService, ShipId shipId, ShipSellRequest request, CancellationToken cancellationToken = default) =>
        {
            var ship = await fleetService.GetShip(shipId, cancellationToken);
            if (ship == null) { return Results.NotFound(); }

            await ship.Sell(request.ItemId, request.Quantity, cancellationToken);
            return Results.Ok(ship);
        });
        shipGroup.MapPost("/wait", async (IFleetService fleetService, ShipId shipId, CancellationToken cancellationToken = default) =>
        {
            var ship = await fleetService.GetShip(shipId, cancellationToken);
            if (ship == null) { return Results.NotFound(); }

            await ship.AwaitArrival(cancellationToken);
            return Results.Ok(ship);
        });

        return app;
    }
}

public record ShipNavigateRequest(WaypointId WaypointId, ShipSpeed? Speed, bool? Wait);
public record ShipSellRequest(ItemId ItemId, int Quantity);