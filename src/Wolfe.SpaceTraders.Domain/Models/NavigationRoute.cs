namespace Wolfe.SpaceTraders.Domain.Models;

public class NavigationRoute
{
    public required Waypoint Destination { get; set; }
    public required Waypoint Departure { get; set; }
    public required DateTimeOffset DepartureTime { get; set; }
    public required DateTimeOffset Arrival { get; set; }
}