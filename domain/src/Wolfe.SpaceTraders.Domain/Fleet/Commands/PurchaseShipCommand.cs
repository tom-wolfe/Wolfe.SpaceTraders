using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Service.Commands;

public class PurchaseShipCommand
{
    public required ShipType ShipType { get; init; }
    public required WaypointId WaypointId { get; init; }
}