using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Domain.Exploration;

internal class AStarWayfinderService : IWayfinderService
{
    public IAsyncEnumerable<WayfinderStop> PlotRoute(Ship ship, WaypointId destination)
    {
        // TODO: Implement A* pathfinding algorithm
        throw new NotImplementedException();
    }
}
