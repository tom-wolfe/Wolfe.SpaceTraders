namespace Wolfe.SpaceTraders.Domain.Exploration;

public class WayfinderPath
{
    public required IReadOnlyCollection<WaypointId> Waypoints { get; init; }

    public required double TotalDistance { get; init; }
}
