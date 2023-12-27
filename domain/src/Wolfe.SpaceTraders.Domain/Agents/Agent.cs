using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Domain.Agents;

public class Agent
{
    public required AgentId Id { get; init; }
    public required AccountId AccountId { get; init; }
    public required WaypointId Headquarters { get; init; }
    public required FactionId FactionId { get; init; }
    public required Credits Credits { get; init; } = Credits.Zero;
}