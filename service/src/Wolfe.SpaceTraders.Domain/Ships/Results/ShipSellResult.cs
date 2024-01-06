using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Marketplaces;

namespace Wolfe.SpaceTraders.Domain.Ships.Results;

public class ShipSellResult
{
    public required Agent Agent { get; init; }
    public required MarketTransaction Transaction { get; init; }
}