namespace Wolfe.SpaceTraders.Sdk.Models.Navigation;

public class SpaceTradersNavigationRoute
{
    public required SpaceTradersWaypointLocation Origin { get; set; }
    public required SpaceTradersWaypointLocation Destination { get; set; }
    public required DateTimeOffset DepartureTime { get; set; }
    public required DateTimeOffset Arrival { get; set; }
}