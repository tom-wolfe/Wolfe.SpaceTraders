using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Marketplaces;

namespace Wolfe.SpaceTraders.Domain.Ships.Results;

public class ShipRefuelResult
{
    public required Agent Agent { get; init; }
    public required ShipFuel Fuel { get; init; }
    public required MarketTransaction Transaction { get; init; }
}