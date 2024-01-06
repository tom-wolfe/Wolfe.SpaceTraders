namespace Wolfe.SpaceTraders.Domain.Exploration;

public interface IWayfinderService
{
    public Task<WayfinderRoute> PlotRoute(WaypointId start, WaypointId destination, CancellationToken cancellationToken = default);
}
