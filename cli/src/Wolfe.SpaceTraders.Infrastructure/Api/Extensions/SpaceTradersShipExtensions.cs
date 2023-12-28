using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Sdk.Models.Ships;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersShipExtensions
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
}