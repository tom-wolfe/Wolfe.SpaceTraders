namespace Wolfe.SpaceTraders.Domain.Models;

public class Agent
{
    public required AccountId AccountId { get; set; }
    public required AgentSymbol Symbol { get; set; }
    public required WaypointSymbol Headquarters { get; set; }
    public required Credits Credits { get; set; } = Credits.Zero;
    public required FactionSymbol StartingFaction { get; set; }
}