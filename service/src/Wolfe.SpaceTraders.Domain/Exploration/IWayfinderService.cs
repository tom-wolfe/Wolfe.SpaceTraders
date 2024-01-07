using Wolfe.SpaceTraders.Domain.General;

namespace Wolfe.SpaceTraders.Domain.Exploration;

public interface IWayfinderService
{
    public Task<WayfinderPath> FindPath(WaypointId start, WaypointId destination, Distance furthestHop, CancellationToken cancellationToken = default);
}
