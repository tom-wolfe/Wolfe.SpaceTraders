namespace Wolfe.SpaceTraders.Sdk.Models.Navigation;

public class SpaceTradersNavigationRoute
{
    public required SpaceTradersWaypoint Destination { get; set; }
    public required SpaceTradersWaypoint Departure { get; set; }
    public required DateTimeOffset DepartureTime { get; set; }
    public required DateTimeOffset Arrival { get; set; }
}