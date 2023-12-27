using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Marketplace;

namespace Wolfe.SpaceTraders.Domain.Ships.Results;

public class ShipRefuelResult
{
    public required Agent Agent { get; set; }
    public required ShipFuel Fuel { get; set; }
    public required Transaction Transaction { get; set; }
}