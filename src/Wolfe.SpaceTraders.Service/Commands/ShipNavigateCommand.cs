using Wolfe.SpaceTraders.Domain.Models;

namespace Wolfe.SpaceTraders.Service.Commands;

public class ShipNavigateCommand
{
    public required WaypointSymbol WaypointSymbol { get; set; }
}