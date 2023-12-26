using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Domain.Shipyards;
using Wolfe.SpaceTraders.Sdk.Models.Shipyards;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersShipyardShipTypeExtensions
{
    public static ShipyardShipType ToDomain(this SpaceTradersShipyardShipType type) => new()
    {
        Type = new ShipType(type.Type)
    };
}