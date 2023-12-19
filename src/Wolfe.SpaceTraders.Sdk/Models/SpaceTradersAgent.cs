namespace Wolfe.SpaceTraders.Sdk.Models;

public class SpaceTradersAgent
{
    public required string AccountId { get; set; }
    public required string Symbol { get; set; }
    public required string Headquarters { get; set; }
    public long Credits { get; set; }
    public required string StartingFaction { get; set; }
}