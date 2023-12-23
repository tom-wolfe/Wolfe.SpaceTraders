using Wolfe.SpaceTraders.Sdk.Models.Navigation;

namespace Wolfe.SpaceTraders.Sdk.Models.Ships;

public class SpaceTradersPatchShipNavResult
{
    public required string SystemSymbol { get; set; }
    public required string WaypointSymbol { get; set; }
    public required string Status { get; set; }
    public required string FlightMode { get; set; }
    public required SpaceTradersNavigationRoute Route { get; set; }
}