using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Domain.Agents;

public class Agent
{
    public required AccountId AccountId { get; init; }
    public required AgentSymbol Symbol { get; init; }
    public required WaypointSymbol Headquarters { get; init; }
    public required Credits Credits { get; init; } = Credits.Zero;
    public required FactionSymbol StartingFaction { get; init; }
}