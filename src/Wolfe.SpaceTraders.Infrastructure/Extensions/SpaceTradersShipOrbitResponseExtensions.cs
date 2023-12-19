using Wolfe.SpaceTraders.Sdk.Responses;
using Wolfe.SpaceTraders.Service.Responses;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersShipOrbitResponseExtensions
{
    public static ShipOrbitResponse ToDomain(this SpaceTradersShipOrbitResponse response) => new()
    {
        Navigation = response.Nav.ToDomain(),
    };
}