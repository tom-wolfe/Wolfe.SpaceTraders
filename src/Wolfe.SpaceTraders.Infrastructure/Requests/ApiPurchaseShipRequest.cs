using Wolfe.SpaceTraders.Core.Requests;

namespace Wolfe.SpaceTraders.Infrastructure.Requests;

internal class ApiPurchaseShipRequest
{
    public required string ShipType { get; set; }
    public required string WaypointSymbol { get; set; }

    public static ApiPurchaseShipRequest FromDomain(PurchaseShipRequest request) => new()
    {
        ShipType = request.ShipType.Value,
        WaypointSymbol = request.WaypointSymbol.Value
    };
}