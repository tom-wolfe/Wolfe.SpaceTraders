using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Sdk.Models.Ships;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersShipExtensions
{
    public static Ship ToDomain(this SpaceTradersShip ship, IShipClient client) => new()
    {
        Id = new ShipId(ship.Symbol),
        Navigation = ship.Nav.ToDomain(),
        Registration = ship.Registration.ToDomain(),
        Fuel = ship.Fuel.ToDomain(),
        Cargo = ship.Cargo.ToDomain(client)
    };
}