namespace Wolfe.SpaceTraders.Core.Models;

public class ShipyardTransaction
{
    public required WaypointSymbol WaypointSymbol { get; set; }
    public required ShipSymbol ShipSymbol { get; set; }
    public required int Price { get; set; }
    public required AgentSymbol AgentSymbol { get; set; }
    public required DateTimeOffset Timestamp { get; set; }
}