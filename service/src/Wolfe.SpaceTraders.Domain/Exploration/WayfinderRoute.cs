namespace Wolfe.SpaceTraders.Domain.Exploration;

public class WayfinderRoute
{
    public required IReadOnlyCollection<WaypointId> Waypoints { get; init; }

    public required double TotalDistance { get; init; }
}
