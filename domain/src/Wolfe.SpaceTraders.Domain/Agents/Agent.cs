using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.General;

namespace Wolfe.SpaceTraders.Domain.Agents;

public class Agent
{
    /// <summary>
    /// ID of the agent.
    /// </summary>
    public required AgentId Id { get; init; }

    /// <summary>
    /// Account ID that is tied to this agent. Only included on your own agent.
    /// </summary>
    public required AccountId AccountId { get; init; } = AccountId.Empty;

    /// <summary>
    /// The headquarters of the agent.
    /// </summary>
    public required WaypointId Headquarters { get; init; }

    /// <summary>
    /// The faction the agent started with.
    /// </summary>
    public required Factions.FactionId FactionId { get; init; }

    /// <summary>
    /// The number of credits the agent has available. Credits can be negative if funds have been overdrawn.
    /// </summary>
    public required Credits Credits { get; init; } = Credits.Zero;
}