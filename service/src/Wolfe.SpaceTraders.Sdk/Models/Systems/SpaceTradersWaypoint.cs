namespace Wolfe.SpaceTraders.Sdk.Models.Systems;

public class SpaceTradersWaypoint
{
    public required string Symbol { get; set; }
    public required string Type { get; set; }
    public required string SystemSymbol { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public required IReadOnlyCollection<SpaceTradersWaypointTrait> Traits { get; set; }
}