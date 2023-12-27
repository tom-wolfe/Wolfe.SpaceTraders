using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Marketplace;

namespace Wolfe.SpaceTraders.Domain.Ships.Results;

public class ShipRefuelResult
{
    public required Agent Agent { get; init; }
    public required ShipFuel Fuel { get; init; }
    public required Transaction Transaction { get; init; }
}