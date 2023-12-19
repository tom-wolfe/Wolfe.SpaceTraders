using Wolfe.SpaceTraders.Sdk.Requests;
using Wolfe.SpaceTraders.Service.Requests;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class ShipNavigateRequestExtensions
{
    public static SpaceTradersShipNavigateRequest ToApi(this ShipNavigateRequest request) => new()
    {
        WaypointSymbol = request.WaypointSymbol.Value,
    };
}