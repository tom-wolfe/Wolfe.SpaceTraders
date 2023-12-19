using Wolfe.SpaceTraders.Sdk.Requests;
using Wolfe.SpaceTraders.Service.Requests;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class PurchaseShipRequestExtensions
{
    public static SpaceTradersPurchaseShipRequest ToApi(this PurchaseShipRequest request) => new()
    {
        WaypointSymbol = request.WaypointSymbol.Value,
        ShipType = request.ShipType.Value,
    };
}