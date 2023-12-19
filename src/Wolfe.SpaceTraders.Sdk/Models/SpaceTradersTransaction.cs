namespace Wolfe.SpaceTraders.Sdk.Models;

public class SpaceTradersTransaction
{
    public required string WaypointSymbol { get; set; }
    public required string ShipSymbol { get; set; }
    public required string TradeSymbol { get; set; }
    public required string Type { get; set; }
    public required int Units { get; set; }
    public required int PricePerUnit { get; set; }
    public required int TotalPrice { get; set; }
    public required DateTimeOffset Timestamp { get; set; }
}