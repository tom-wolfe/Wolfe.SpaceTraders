using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Domain.Models.Ships;
using Wolfe.SpaceTraders.Domain.Models.Shipyards;
using Wolfe.SpaceTraders.Sdk.Models.Shipyards;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersShipyardShipTypeExtensions
{
    public static ShipyardShipType ToDomain(this SpaceTradersShipyardShipType type) => new()
    {
        Type = new ShipType(type.Type)
    };
}