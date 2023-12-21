using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Domain.Models;

namespace Wolfe.SpaceTraders.Service.Responses;

public class ShipRefuelResponse
{
    public required Agent Agent { get; set; }
    public required ShipFuel Fuel { get; set; }
    public required MarketplaceTransaction Transaction { get; set; }
}