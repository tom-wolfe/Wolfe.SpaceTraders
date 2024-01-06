using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.General;

namespace Wolfe.SpaceTraders.Domain.Ships;

public class ShipNavigationDestination
{
    /// <summary>
    /// Buffer used for clock skew when comparing arrival times.
    /// </summary>
    private static readonly TimeSpan ClockSkew = TimeSpan.FromSeconds(2);

    public required WaypointId WaypointId { get; init; }
    public required Point Location { get; init; }
    public required DateTimeOffset Arrival { get; init; }
    public TimeSpan TimeToArrival => Arrival + ClockSkew - DateTimeOffset.UtcNow;
}