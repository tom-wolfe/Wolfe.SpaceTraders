using Wolfe.SpaceTraders.Core.Models;

namespace Wolfe.SpaceTraders.Core.Requests;

public class PurchaseShipRequest
{
    public required ShipType ShipType { get; set; }
    public required WaypointSymbol WaypointSymbol { get; set; }
}