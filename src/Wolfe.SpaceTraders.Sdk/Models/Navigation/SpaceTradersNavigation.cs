namespace Wolfe.SpaceTraders.Sdk.Models.Navigation;

public class SpaceTradersNavigation
{
    public required string SystemSymbol { get; set; }
    public required string WaypointSymbol { get; set; }
    public required SpaceTradersNavigationRoute Route { get; set; }
    public required string Status { get; set; }
    public required string FlightMode { get; set; }
}