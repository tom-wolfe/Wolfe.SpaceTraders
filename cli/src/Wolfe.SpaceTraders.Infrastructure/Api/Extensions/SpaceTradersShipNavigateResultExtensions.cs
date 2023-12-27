using Wolfe.SpaceTraders.Domain.Ships.Results;
using Wolfe.SpaceTraders.Sdk.Models.Ships;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersShipNavigateResultExtensions
{
    public static ShipNavigateResult ToDomain(this SpaceTradersShipNavigateResult result) => new()
    {
        Navigation = result.Nav.ToDomain(),
        Fuel = result.Fuel.ToDomain(),
    };
}