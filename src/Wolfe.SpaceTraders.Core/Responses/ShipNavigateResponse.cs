using Wolfe.SpaceTraders.Core.Models;

namespace Wolfe.SpaceTraders.Core.Responses;

public class ShipNavigateResponse
{
    public required ShipFuel Fuel { get; set; }
    public required ShipNav Nav { get; set; }
}