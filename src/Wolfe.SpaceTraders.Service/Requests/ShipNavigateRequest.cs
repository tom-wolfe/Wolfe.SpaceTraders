using Wolfe.SpaceTraders.Core.Models;

namespace Wolfe.SpaceTraders.Service.Requests;

public class ShipNavigateRequest
{
    public required WaypointSymbol WaypointSymbol { get; set; }
}