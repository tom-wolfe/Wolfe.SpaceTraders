using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Domain.Fleet.Commands;

public class PurchaseShipCommand
{
    public required ShipType ShipType { get; init; }
    public required WaypointId ShipyardId { get; init; }
}