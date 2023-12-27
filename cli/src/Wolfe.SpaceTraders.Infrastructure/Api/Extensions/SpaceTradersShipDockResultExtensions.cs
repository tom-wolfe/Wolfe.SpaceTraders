using Wolfe.SpaceTraders.Domain.Ships.Results;
using Wolfe.SpaceTraders.Sdk.Models.Ships;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersShipDockResultExtensions
{
    public static ShipDockResult ToDomain(this SpaceTradersShipDockResult result) => new()
    {
        Navigation = result.Nav.ToDomain(),
    };
}