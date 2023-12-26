using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Service.Commands;

public class ShipNavigateCommand
{
    public required WaypointSymbol WaypointSymbol { get; set; }
}