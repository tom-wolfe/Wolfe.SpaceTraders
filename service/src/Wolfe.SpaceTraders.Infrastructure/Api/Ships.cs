using System.Data;
using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.General;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Domain.Ships.Commands;
using Wolfe.SpaceTraders.Domain.Ships.Results;
using Wolfe.SpaceTraders.Sdk.Models.Extraction;
using Wolfe.SpaceTraders.Sdk.Models.Ships;
using Wolfe.SpaceTraders.Sdk.Requests;

namespace Wolfe.SpaceTraders.Infrastructure.Api;

internal static class Ships
{
    public static Ship ToDomain(this SpaceTradersShip ship, AgentId agentId, IShipClient client) => Ship.Create(
        client: client,
        shipId: new ShipId(ship.Symbol),
        agentId: agentId,
        name: ship.Registration.Name,
        role: new ShipRole(ship.Registration.Role),
        fuel: ship.Fuel.ToDomain(),
        navigation: ship.Nav.ToDomain(),
        cargo: ship.Cargo.ToDomain()
    );

    public static IShipCargoBase ToDomain(this SpaceTradersShipCargo cargo) => new SpaceTradersShipCargoBase(
        Capacity: cargo.Capacity,
        Items: cargo.Inventory.Select(i => i.ToDomain()).ToList()
    );

    public static IShipFuelBase ToDomain(this SpaceTradersShipFuel fuel) => new SpaceTradersShipFuelBase(
        Current: new Fuel(fuel.Current),
        Capacity: new Fuel(fuel.Capacity)
    );

    public static ShipCooldown ToDomain(this SpaceTradersShipCooldown cooldown) => new()
    {
        Expiration = cooldown.Expiration,
        Total = TimeSpan.FromSeconds(cooldown.TotalSeconds),
    };

    public static ShipExtractResult ToDomain(this SpaceTradersShipExtractResult result) => new()
    {
        Cooldown = result.Cooldown.ToDomain(),
        Yield = result.Extraction.Yield.ToDomain(result.Cargo),
    };

    public static ShipNavigateResult ToDomain(this SpaceTradersShipNavigateResult result) => new()
    {
        Navigation = result.Nav.ToDomain(),
        Fuel = result.Fuel.ToDomain(),
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
        Transaction = result.Transaction.ToDomain()
    };

    public static ShipCargoItem ToDomain(this SpaceTradersCargoItem item) => new()
    {
        Id = new ItemId(item.Symbol),
        Name = item.Name,
        Description = item.Description,
        Quantity = item.Units,
    };

    public static ShipCargoItem ToDomain(this SpaceTradersExtractionYield yield, SpaceTradersShipCargo cargo) => new()
    {
        Id = new ItemId(yield.Symbol),
        Name = cargo.Inventory.First(f => f.Symbol == yield.Symbol).Name,
        Description = cargo.Inventory.First(f => f.Symbol == yield.Symbol).Description,
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

    public static IShipNavigation ToDomain(this SpaceTradersShipNav navigation) => new SpaceTradersShipNavigation
    {
        WaypointId = new WaypointId(navigation.WaypointSymbol),
        Location = new Point(navigation.Route.Origin.X, navigation.Route.Origin.Y),
        Status = new ShipNavigationStatus(navigation.Status),
        Speed = new ShipSpeed(navigation.FlightMode),
        Destination = navigation.Route.Origin.Symbol == navigation.Route.Destination.Symbol ? null : new ShipNavigationDestination
        {
            Location = new Point(navigation.Route.Destination.X, navigation.Route.Destination.Y),
            WaypointId = new(navigation.Route.Destination.Symbol),
            Arrival = navigation.Route.Arrival
        },
    };

    private record SpaceTradersShipCargoBase(int Capacity, IReadOnlyCollection<ShipCargoItem> Items) : IShipCargoBase;
    private record SpaceTradersShipFuelBase(Fuel Current, Fuel Capacity) : IShipFuelBase;

    private record SpaceTradersShipNavigation : IShipNavigation
    {
        public WaypointId WaypointId { get; init; }
        public Point Location { get; init; }
        public ShipNavigationStatus Status { get; init; }
        public ShipSpeed Speed { get; init; }
        public ShipNavigationDestination? Destination { get; init; }
    }
}