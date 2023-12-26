using Wolfe.SpaceTraders.Domain.Marketplace;
using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Domain.Contracts;

public class ContractGood
{
    public required TradeSymbol TradeSymbol { get; init; }
    public required WaypointSymbol DestinationSymbol { get; init; }
    public int UnitsRequired { get; init; }
    public int UnitsFulfilled { get; init; }
}