using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Sdk.Models.Shipyards;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersShipyardShipExtensions
{
    public static ShipyardShip ToDomain(this SpaceTradersShipyardShip ship) => new()
    {
        Type = new ShipType(ship.Type),
        Description = ship.Description,
        Name = ship.Name,
        PurchasePrice = new Credits(ship.PurchasePrice)
    };
}