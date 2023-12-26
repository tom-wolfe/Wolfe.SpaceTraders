namespace Wolfe.SpaceTraders.Domain.Navigation;

public class NavigationRoute
{
    public required WaypointLocation Destination { get; init; }
    public required WaypointLocation Origin { get; init; }
    public required DateTimeOffset DepartureTime { get; init; }
    public required DateTimeOffset Arrival { get; init; }
    public TimeSpan ArrivesIn => Arrival - DateTimeOffset.UtcNow;
}