using Wolfe.SpaceTraders.Sdk.Models.Ships;
using Wolfe.SpaceTraders.Service.Results;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersShipOrbitResultExtensions
{
    public static ShipOrbitResult ToDomain(this SpaceTradersShipOrbitResult result) => new()
    {
        Navigation = result.Nav.ToDomain(),
    };
}