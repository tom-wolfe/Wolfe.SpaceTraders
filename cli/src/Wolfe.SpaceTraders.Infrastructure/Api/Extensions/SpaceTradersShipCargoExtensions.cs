using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Sdk.Models.Ships;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersShipCargoExtensions
{
    public static ShipCargo ToDomain(this SpaceTradersShipCargo cargo) => new()
    {
        Quantity = cargo.Units,
        Capacity = cargo.Capacity,
        Items = cargo.Inventory.Select(i => i.ToDomain()).ToList()
    };
}