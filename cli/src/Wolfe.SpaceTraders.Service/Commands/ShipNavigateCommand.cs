using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Service.Commands;

public class ShipNavigateCommand
{
    public required WaypointId WaypointId { get; set; }
}