namespace Wolfe.SpaceTraders.Sdk.Models.Ships;

public class SpaceTradersShipNavRoute
{
    public required SpaceTradersShipNavRouteWaypoint Origin { get; set; }
    public required SpaceTradersShipNavRouteWaypoint Destination { get; set; }
    public required DateTimeOffset DepartureTime { get; set; }
    public required DateTimeOffset Arrival { get; set; }
}