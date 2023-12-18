using Wolfe.SpaceTraders.Core.Requests;

namespace Wolfe.SpaceTraders.Infrastructure.Requests;

internal class ApiShipNavigateRequest
{
    public required string WaypointSymbol { get; set; }

    public static ApiShipNavigateRequest FromDomain(ShipNavigateRequest request) => new()
    {
        WaypointSymbol = request.WaypointSymbol.Value
    };
}