using Wolfe.SpaceTraders.Domain.Models;

namespace Wolfe.SpaceTraders.Service.Requests;

public class ShipNavigateRequest
{
    public required WaypointSymbol WaypointSymbol { get; set; }
}