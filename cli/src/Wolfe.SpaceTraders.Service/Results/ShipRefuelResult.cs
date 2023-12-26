using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Marketplace;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Service.Results;

public class ShipRefuelResult
{
    public required Agent Agent { get; set; }
    public required ShipFuel Fuel { get; set; }
    public required Transaction Transaction { get; set; }
}