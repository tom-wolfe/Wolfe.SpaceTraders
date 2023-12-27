using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Marketplace;

namespace Wolfe.SpaceTraders.Domain.Ships.Results;

public class ShipSellResult
{
    public required Agent Agent { get; init; }
    public required ShipCargo Cargo { get; init; }
    public required Transaction Transaction { get; init; }
}