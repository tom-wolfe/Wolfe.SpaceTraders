namespace Wolfe.SpaceTraders.Domain.Models;

public class Faction
{
    public FactionSymbol Symbol { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required WaypointSymbol Headquarters { get; set; }

}