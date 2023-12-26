using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Domain.Shipyards;

public class ShipyardTransaction
{
    public required WaypointSymbol WaypointSymbol { get; init; }
    public required ShipSymbol ShipSymbol { get; init; }
    public required Credits Price { get; init; }
    public required AgentSymbol AgentSymbol { get; init; }
    public required DateTimeOffset Timestamp { get; init; }
}