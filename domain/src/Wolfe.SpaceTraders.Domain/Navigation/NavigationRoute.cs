namespace Wolfe.SpaceTraders.Domain.Navigation;

public class NavigationRoute
{
    public required WaypointLocation Destination { get; set; }
    public required WaypointLocation Origin { get; set; }
    public required DateTimeOffset DepartureTime { get; set; }
    public required DateTimeOffset Arrival { get; set; }
    public TimeSpan ArrivesIn => Arrival - DateTimeOffset.UtcNow;
}