namespace Wolfe.SpaceTraders.Domain.Navigation;

public class NavigationRoute
{
    /// <summary>
    /// Buffer used for clock skew when comparing arrival times.
    /// </summary>
    private static readonly TimeSpan ClockSkew = TimeSpan.FromSeconds(2);

    public required WaypointLocation Destination { get; init; }
    public required WaypointLocation Origin { get; init; }
    public required DateTimeOffset DepartureTime { get; init; }
    public required DateTimeOffset Arrival { get; init; }
    public TimeSpan TimeToArrival => (Arrival + ClockSkew) - DateTimeOffset.UtcNow;
}