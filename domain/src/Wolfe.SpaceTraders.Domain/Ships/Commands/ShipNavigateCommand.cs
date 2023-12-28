using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Domain.Ships.Commands;

public class ShipNavigateCommand
{
    public required WaypointId WaypointId { get; init; }
}