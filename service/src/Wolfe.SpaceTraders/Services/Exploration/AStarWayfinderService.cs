using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Marketplaces;

namespace Wolfe.SpaceTraders.Services.Exploration;

internal class AStarWayfinderService(
    IExplorationService explorationService,
    IMarketplaceService marketplaceService
) : IWayfinderService
{
    public async Task<WayfinderRoute> PlotRoute(WaypointId start, WaypointId destination, CancellationToken cancellationToken = default)
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

        var openSet = new List<Waypoint> { first };

        var cameFrom = new Dictionary<Waypoint, Waypoint>();

        var gScore = new Dictionary<Waypoint, double>
        {
            [first] = 0
        };

        var fScore = new Dictionary<Waypoint, double>
        {
            [first] = HeuristicCostEstimate(first, goal)
        };

        while (openSet.Count > 0)
        {
            var current = openSet.MinBy(w => fScore.TryGetValue(w, out var score) ? score : double.MaxValue)!;
            if (current == goal)
            {
                return ReconstructPath(cameFrom, current);
            }

            openSet.Remove(current);
            foreach (var neighbour in graph)
            {
                var tentativeGScore = gScore[current] + current.Location.DistanceTo(neighbour.Location).Total;
                if (tentativeGScore < (gScore.TryGetValue(neighbour, out var score) ? score : double.MaxValue))
                {
                    cameFrom[neighbour] = current;
                    gScore[neighbour] = tentativeGScore;
                    fScore[neighbour] = gScore[neighbour] + HeuristicCostEstimate(neighbour, goal);
                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }

        throw new Exception("Unable to find route.");
    }

    private double HeuristicCostEstimate(Waypoint start, Waypoint end)
    {
        return start.Location.DistanceTo(end.Location).Total;
    }

    private WayfinderRoute ReconstructPath(IReadOnlyDictionary<Waypoint, Waypoint> cameFrom, Waypoint current)
    {
        var totalPath = new List<Waypoint> { current };
        var totalDistance = 0.0;
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            totalDistance += current.Location.DistanceTo(totalPath.Last().Location).Total;
            totalPath.Add(current);
        }
        return new WayfinderRoute
        {
            Waypoints = totalPath.Select(i => i.Id).ToList(),
            TotalDistance = totalDistance
        };
    }
}
