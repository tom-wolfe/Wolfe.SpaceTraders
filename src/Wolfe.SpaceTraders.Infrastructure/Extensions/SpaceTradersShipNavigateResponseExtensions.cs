using Wolfe.SpaceTraders.Sdk.Responses;
using Wolfe.SpaceTraders.Service.Responses;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersShipNavigateResponseExtensions
{
    public static ShipNavigateResponse ToDomain(this SpaceTradersShipNavigateResponse response) => new()
    {
        Navigation = response.Nav.ToDomain(),
        Fuel = response.Fuel.ToDomain(),
    };
}