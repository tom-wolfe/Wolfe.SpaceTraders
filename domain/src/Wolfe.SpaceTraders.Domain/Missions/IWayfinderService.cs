using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Domain.Missions;

internal interface IWayfinderService
{
    public IAsyncEnumerable<WayfinderStop> PlotRoute(Ship ship, WaypointId destination);
}

public record WayfinderStop(WaypointId Waypoint, bool Refuel);

