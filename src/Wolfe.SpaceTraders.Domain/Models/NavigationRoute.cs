namespace Wolfe.SpaceTraders.Domain.Models;

public class NavigationRoute
{
    public required WaypointLocation Destination { get; set; }
    public required WaypointLocation Departure { get; set; }
    public required DateTimeOffset DepartureTime { get; set; }
    public required DateTimeOffset Arrival { get; set; }
}