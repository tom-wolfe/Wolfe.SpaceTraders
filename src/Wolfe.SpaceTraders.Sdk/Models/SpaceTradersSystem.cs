namespace Wolfe.SpaceTraders.Sdk.Models;

public class SpaceTradersSystem
{
    public required string Symbol { get; set; }
    public required string SectorSymbol { get; set; }
    public required string Type { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public required IReadOnlyCollection<SpaceTradersWaypoint> Waypoints { get; set; }
    public required IReadOnlyCollection<SpaceTradersFaction> Factions { get; set; }
}
