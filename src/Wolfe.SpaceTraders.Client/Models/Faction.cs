namespace Wolfe.SpaceTraders.Models;

public class Faction
{
    public FactionSymbol Symbol { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Headquarters { get; set; }
    // TODO: Faction traits
    public bool IsRecruiting { get; set; }

}