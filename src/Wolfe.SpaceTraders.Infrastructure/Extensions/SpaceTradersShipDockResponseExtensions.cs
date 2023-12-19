using Wolfe.SpaceTraders.Sdk.Responses;
using Wolfe.SpaceTraders.Service.Responses;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersShipDockResponseExtensions
{
    public static ShipDockResponse ToDomain(this SpaceTradersShipDockResponse response) => new()
    {
        Navigation = response.Nav.ToDomain(),

    };
}