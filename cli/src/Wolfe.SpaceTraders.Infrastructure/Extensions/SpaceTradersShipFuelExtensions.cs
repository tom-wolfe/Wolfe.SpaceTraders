using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Domain.Models.Ships;
using Wolfe.SpaceTraders.Sdk.Models.Ships;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersShipFuelExtensions
{
    public static ShipFuel ToDomain(this SpaceTradersShipFuel fuel) => new()
    {
        Capacity = new Fuel(fuel.Capacity),
        Current = new Fuel(fuel.Current),
        Consumed = fuel.Consumed?.ToDomain()
    };
}