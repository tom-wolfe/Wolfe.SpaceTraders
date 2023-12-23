using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Domain.Models.Waypoints;

namespace Wolfe.SpaceTraders.Service.Commands;

public class ShipNavigateCommand
{
    public required WaypointSymbol WaypointSymbol { get; set; }
}