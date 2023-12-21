using Wolfe.SpaceTraders.Domain.Models.Ships;
using Wolfe.SpaceTraders.Sdk.Models.Ships;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersShipCargoExtensions
{
    public static ShipCargo ToDomain(this SpaceTradersShipCargo cargo) => new()
    {
        Units = cargo.Units,
        Capacity = cargo.Capacity,
        Inventory = cargo.Inventory.Select(i => i.ToDomain()).ToList()
    };
}