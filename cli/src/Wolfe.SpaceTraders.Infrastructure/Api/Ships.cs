using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.General;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Domain.Ships.Commands;
using Wolfe.SpaceTraders.Domain.Ships.Results;
using Wolfe.SpaceTraders.Sdk.Models.Extraction;
using Wolfe.SpaceTraders.Sdk.Models.Jettison;
using Wolfe.SpaceTraders.Sdk.Models.Ships;
using Wolfe.SpaceTraders.Sdk.Requests;

namespace Wolfe.SpaceTraders.Infrastructure.Api;

internal static class Ships
{
    public static Ship ToDomain(this SpaceTradersShip ship, IShipClient client) => new(
        client,
        ship.Cargo.ToDomain(),
        ship.Fuel.ToDomain(),
        ship.Nav.ToDomain()
    )
    {
        Id = new ShipId(ship.Symbol),
        Name = ship.Registration.Name,
        Role = new ShipRole(ship.Registration.Role),
    };

    public static ShipCargo ToDomain(this SpaceTradersShipCargo cargo) => new()
    {
        Quantity = cargo.Units,
        Capacity = cargo.Capacity,
        Items = cargo.Inventory.Select(i => i.ToDomain()).ToList()
    };

    public static ShipFuelConsumed ToDomain(this SpaceTradersShipFuelConsumed consumed) => new()
    {
        Amount = new Fuel(consumed.Amount),
        Timestamp = consumed.Timestamp,
    };

    public static ShipFuel ToDomain(this SpaceTradersShipFuel fuel) => new()
    {
        Capacity = new Fuel(fuel.Capacity),
        Current = new Fuel(fuel.Current),
        Consumed = fuel.Consumed?.ToDomain()
    };

    public static ShipCooldown ToDomain(this SpaceTradersShipCooldown cooldown) => new()
    {
        Expiration = cooldown.Expiration,
        Total = TimeSpan.FromSeconds(cooldown.TotalSeconds),
    };

    public static ShipExtractResult ToDomain(this SpaceTradersShipExtractResult result) => new()
    {
        Cooldown = result.Cooldown.ToDomain(),
        Yield = result.Extraction.Yield.ToDomain(),
        Cargo = result.Cargo.ToDomain(),
    };

    public static ShipDockResult ToDomain(this SpaceTradersShipDockResult result) => new()
    {
        Navigation = result.Nav.ToDomain(),
    };

    public static ShipJettisonResult ToDomain(this SpaceTradersShipJettisonResult result) => new()
    {
        Cargo = result.Cargo.ToDomain()
    };

    public static ShipNavigateResult ToDomain(this SpaceTradersShipNavigateResult result) => new()
    {
        Navigation = result.Nav.ToDomain(),
        Fuel = result.Fuel.ToDomain(),
    };

    public static ShipOrbitResult ToDomain(this SpaceTradersShipOrbitResult result) => new()
    {
        Navigation = result.Nav.ToDomain(),
    };

    public static ShipRefuelResult ToDomain(this SpaceTradersShipRefuelResult result) => new()
    {
        Fuel = result.Fuel.ToDomain(),
        Agent = result.Agent.ToDomain(),
        Transaction = result.Transaction.ToDomain(),
    };

    public static ShipSellResult ToDomain(this SpaceTradersShipSellResult result) => new()
    {
        Agent = result.Agent.ToDomain(),
        Cargo = result.Cargo.ToDomain(),
        Transaction = result.Transaction.ToDomain()
    };

    public static ShipCargoItem ToDomain(this SpaceTradersCargoItem item) => new()
    {
        Id = new ItemId(item.Symbol),
        Name = item.Name,
        Description = item.Description,
        Quantity = item.Units,
    };

    public static ExtractionYield ToDomain(this SpaceTradersExtractionYield yield) => new()
    {
        ItemId = new ItemId(yield.Symbol),
        Quantity = yield.Units,
    };

    public static SpaceTradersShipNavigateRequest ToApi(this ShipNavigateCommand command) => new()
    {
        WaypointSymbol = command.WaypointId.Value,
    };

    public static SpaceTradersShipJettisonRequest ToApi(this ShipJettisonCommand command) => new()
    {
        Symbol = command.ItemId.Value,
        Units = command.Quantity
    };

    public static ShipNavigationRoute ToDomain(this SpaceTradersShipNavRoute route) => new()
    {
        Arrival = route.Arrival,
        Origin = route.Origin.ToDomain(),
        Destination = route.Destination.ToDomain(),
        DepartureTime = route.DepartureTime,
    };

    public static ShipNavigation ToDomain(this SpaceTradersShipNav navigation) => new()
    {
        WaypointId = new WaypointId(navigation.WaypointSymbol),
        Status = new ShipNavigationStatus(navigation.Status),
        Speed = new ShipSpeed(navigation.FlightMode),
        Route = navigation.Route.ToDomain()
    };

    public static ShipNavigationRouteWaypoint ToDomain(this SpaceTradersShipNavRouteWaypoint location) => new()
    {
        Type = new WaypointType(location.Type),
        Id = new WaypointId(location.Symbol),
        SystemId = new SystemId(location.SystemSymbol),
        Point = new Point(location.X, location.Y),
    };
}