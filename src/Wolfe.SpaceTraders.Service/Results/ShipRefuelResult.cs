using Wolfe.SpaceTraders.Domain.Models;

namespace Wolfe.SpaceTraders.Service.Results;

public class ShipRefuelResult
{
    public required Agent Agent { get; set; }
    public required ShipFuel Fuel { get; set; }
    public required MarketplaceTransaction Transaction { get; set; }
}