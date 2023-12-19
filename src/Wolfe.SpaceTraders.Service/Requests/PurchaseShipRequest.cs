using Wolfe.SpaceTraders.Core.Models;

namespace Wolfe.SpaceTraders.Service.Requests;

public class PurchaseShipRequest
{
    public required ShipType ShipType { get; set; }
    public required WaypointSymbol WaypointSymbol { get; set; }
}