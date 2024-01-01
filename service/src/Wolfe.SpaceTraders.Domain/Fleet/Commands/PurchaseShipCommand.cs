using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Domain.Fleet.Commands;

public class PurchaseShipCommand
{
    public required ShipType ShipType { get; init; }
    public required WaypointId ShipyardId { get; init; }
}