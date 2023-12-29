using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Domain.Exploration;

public interface IWayfinderService
{
    public IAsyncEnumerable<WayfinderStop> PlotRoute(Ship ship, WaypointId destination);
}

public record WayfinderStop(WaypointId Waypoint, bool Refuel);
