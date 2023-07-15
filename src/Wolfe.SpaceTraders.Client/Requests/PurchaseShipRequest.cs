using Wolfe.SpaceTraders.Models;

namespace Wolfe.SpaceTraders.Requests;

public class PurchaseShipRequest
{
    public required ShipType ShipType { get; set; }
    public required WaypointSymbol WaypointSymbol { get; set; }
}