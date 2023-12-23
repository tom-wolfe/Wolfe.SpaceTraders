namespace Wolfe.SpaceTraders.Sdk.Models.Shipyards;

public class SpaceTradersShipyardTransaction
{
    public required string WaypointSymbol { get; set; }
    public required string ShipSymbol { get; set; }
    public required long Price { get; set; }
    public required string AgentSymbol { get; set; }
    public required DateTimeOffset Timestamp { get; set; }
}