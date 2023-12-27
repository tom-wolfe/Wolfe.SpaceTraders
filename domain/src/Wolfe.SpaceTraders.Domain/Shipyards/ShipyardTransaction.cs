using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Domain.Shipyards;

public class ShipyardTransaction
{
    public required WaypointId WaypointId { get; init; }
    public required ShipId ShipId { get; init; }
    public required Credits Price { get; init; }
    public required AgentId AgentId { get; init; }
    public required DateTimeOffset Timestamp { get; init; }
}