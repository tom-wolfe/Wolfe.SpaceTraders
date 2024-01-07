using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.General;
using Wolfe.SpaceTraders.Domain.Marketplaces;

namespace Wolfe.SpaceTraders.Services.Exploration;

internal class AStarWayfinderService(
    IExplorationService explorationService,
    IMarketplaceService marketplaceService
) : IWayfinderService
{
    private static readonly Distance MinimumDistance = new(1, 1);

    public async Task<WayfinderPath> FindPath(WaypointId start, WaypointId destination, Distance furthestHop, CancellationToken cancellationToken = default)
    {
        var first = await explorationService.GetWaypoint(start, cancellationToken) ?? throw new Exception("Unable to find starting waypoint.");
        var goal = await explorationService.GetWaypoint(destination, cancellationToken) ?? throw new Exception("Unable to find destination waypoint.");

        var graph = await marketplaceService
            .GetMarketplaces(first.SystemId, cancellationToken)
            .Where(x => x.IsSelling(ItemId.Fuel))
            .Cast<Waypoint>()
            .ToListAsync(cancellationToken);

        if (!graph.Exists(m => m.Id == first.Id)) { graph.Add(first); }
        if (!graph.Exists(m => m.Id == goal.Id)) { graph.Add(goal); }


        var aStar = new AStarSearch<Waypoint>(
            neighbors: from => graph.Where(to => from.Id != to.Id && CalculateDistance(from, to) < furthestHop),
            distance: DistanceValue,
            heuristic: DistanceValue
        );

        var path = aStar.Search(first, goal);

        return new WayfinderPath
        {
            Waypoints = path.Nodes.Select(p => p.Id).ToList(),
            TotalDistance = path.TotalCost
        };

        double DistanceValue(Waypoint from, Waypoint to) => CalculateDistance(from, to).Total;
    }

    private static Distance CalculateDistance(Waypoint from, Waypoint to)
    {
        // Waypoints in the same location still have a minimum travel time.
        var distance = from.Location.DistanceTo(to.Location);
        return (distance == Distance.Zero) ? MinimumDistance : distance;
    }
}
