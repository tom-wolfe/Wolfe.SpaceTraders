using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Domain.Models.Ships;
using Wolfe.SpaceTraders.Domain.Models.Waypoints;

namespace Wolfe.SpaceTraders.Service.Commands;

public class PurchaseShipCommand
{
    public required ShipType ShipType { get; set; }
    public required WaypointSymbol WaypointSymbol { get; set; }
}