namespace Wolfe.SpaceTraders.Models;

public class Agent
{
    public required string AccountId { get; set; }
    public required AgentSymbol Symbol { get; set; }
    public required string Headquarters { get; set; }
    public long Credits { get; set; }
    public required FactionSymbol StartingFaction { get; set; }
    public int ShipCount { get; set; }
}