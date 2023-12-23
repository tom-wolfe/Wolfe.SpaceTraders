using Wolfe.SpaceTraders.Sdk.Models.Ships;
using Wolfe.SpaceTraders.Service.Results;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersShipDockResultExtensions
{
    public static ShipDockResult ToDomain(this SpaceTradersShipDockResult result) => new()
    {
        Navigation = result.Nav.ToDomain(),
    };
}