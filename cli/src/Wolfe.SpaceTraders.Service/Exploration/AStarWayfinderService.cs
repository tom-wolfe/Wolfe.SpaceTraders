using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Service.Exploration;

internal class AStarWayfinderService : IWayfinderService
{
    public IAsyncEnumerable<WayfinderStop> PlotRoute(Ship ship, WaypointId destination)
    {
        // TODO: Implement A* pathfinding algorithm
        throw new NotImplementedException();
    }
}
