using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Sdk.Models.Ships;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersShipExtensions
{
    public static Ship ToDomain(this SpaceTradersShip ship) => new()
    {
        Symbol = new ShipSymbol(ship.Symbol),
        Navigation = ship.Nav.ToDomain(),
        Registration = ship.Registration.ToDomain(),
        Fuel = ship.Fuel.ToDomain(),
        Cargo = ship.Cargo.ToDomain()
    };
}