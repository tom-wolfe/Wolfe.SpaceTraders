namespace Wolfe.SpaceTraders.Sdk.Models.Ships;

public class SpaceTradersShipNav
{
    public required string SystemSymbol { get; set; }
    public required string WaypointSymbol { get; set; }
    public required SpaceTradersShipNavRoute Route { get; set; }
    public required string Status { get; set; }
    public required string FlightMode { get; set; }
}