using Wolfe.SpaceTraders.Core.Models;

namespace Wolfe.SpaceTraders.Core.Requests;

public class ShipNavigateRequest
{
    public required WaypointSymbol WaypointSymbol { get; set; }
}