using Wolfe.SpaceTraders.Sdk.Models.Ships;
using Wolfe.SpaceTraders.Service.Results;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersShipNavigateResultExtensions
{
    public static ShipNavigateResult ToDomain(this SpaceTradersShipNavigateResult result) => new()
    {
        Navigation = result.Nav.ToDomain(),
        Fuel = result.Fuel.ToDomain(),
    };
}