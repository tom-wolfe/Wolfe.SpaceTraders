namespace Wolfe.SpaceTraders.Sdk.Models.Navigation;

public class SpaceTradersWaypointLocation
{
    public required string Symbol { get; set; }
    public required string Type { get; set; }
    public required string SystemSymbol { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}