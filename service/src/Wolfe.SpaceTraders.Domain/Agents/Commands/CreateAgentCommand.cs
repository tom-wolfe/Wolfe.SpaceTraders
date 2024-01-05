using Wolfe.SpaceTraders.Domain.Factions;

namespace Wolfe.SpaceTraders.Domain.Agents.Commands;

public class CreateAgentCommand
{
    public required FactionId Faction { get; init; }
    public required AgentId Agent { get; init; }
    public string? Email { get; init; }
}