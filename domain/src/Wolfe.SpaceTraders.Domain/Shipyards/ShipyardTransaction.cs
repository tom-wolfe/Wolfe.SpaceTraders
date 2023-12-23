using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Domain.Shipyards;

public class ShipyardTransaction
{
    public required WaypointSymbol WaypointSymbol { get; set; }
    public required ShipSymbol ShipSymbol { get; set; }
    public required Credits Price { get; set; }
    public required AgentSymbol AgentSymbol { get; set; }
    public required DateTimeOffset Timestamp { get; set; }
}