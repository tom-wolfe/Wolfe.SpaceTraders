namespace Wolfe.SpaceTraders.Domain.Exploration;

public class WayfinderRoute
{
    public IReadOnlyCollection<WaypointId> Waypoints { get; init; }

    public double TotalDistance { get; init; }
}
