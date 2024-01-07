namespace Wolfe.SpaceTraders.Services.Exploration;

public class AStarSearch<T>(
    Func<T, IEnumerable<T>> neighbors,
    Func<T, T, double> distance,
    Func<T, T, double> heuristic
) where T : notnull
{
    public AStarPath<T> Search(T start, T goal)
    {
        var nodesToSearch = new List<T> { start };
        var cameFrom = new Dictionary<T, T>();

        var cheapestToN = new Dictionary<T, double> { [start] = 0 };
        var bestGuessUsingN = new Dictionary<T, double> { [start] = heuristic(start, goal) };

        while (nodesToSearch.Count > 0)
        {
            var current = nodesToSearch.MinBy(n => bestGuessUsingN.TryGetValue(n, out var score) ? score : double.MaxValue)!;
            if (current.Equals(goal))
            {
                return ReconstructPath(cameFrom, current, cheapestToN[current]);
            }

            nodesToSearch.Remove(current);

            foreach (var neighbor in neighbors(current))
            {
                var cheapestToCurrent = cheapestToN.TryGetValue(current, out var cScore) ? cScore : double.MaxValue;
                var cheapestToNeighbor = cheapestToN.TryGetValue(neighbor, out var nScore) ? nScore : double.MaxValue;

                var currentToNeighbor = cheapestToCurrent + distance(current, neighbor);
                if (currentToNeighbor >= cheapestToNeighbor)
                {
                    continue;
                }

                cameFrom[neighbor] = current;
                cheapestToN[neighbor] = currentToNeighbor;
                bestGuessUsingN[neighbor] = currentToNeighbor + heuristic(neighbor, goal);
                if (!nodesToSearch.Contains(neighbor))
                {
                    nodesToSearch.Add(neighbor);
                }
            }
        }

        throw new Exception("Unable to find route.");
    }

    private static AStarPath<T> ReconstructPath(Dictionary<T, T> cameFrom, T current, double totalCost)
    {
        var totalPath = new List<T> { current };
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            totalPath.Insert(0, current);
        }

        return new AStarPath<T>
        {
            Nodes = totalPath,
            TotalCost = totalCost
        };
    }
}

public class AStarPath<T>
{
    public required IReadOnlyCollection<T> Nodes { get; init; }

    public required double TotalCost { get; init; }
}
