using Wolfe.SpaceTraders.Domain.Models;

namespace Wolfe.SpaceTraders.Service.Results;

public class ShipNavigateResult
{
    public required ShipFuel Fuel { get; set; }
    public required Navigation Navigation { get; set; }
}