using FluentAssertions;
using FluentAssertions.Execution;
using Wolfe.SpaceTraders.Services.Exploration;

namespace Wolfe.SpaceTraders.Tests.Unit.Services.Exploration;

public class AStarSearchTests
{
    [Fact]
    public void KnownExample1()
    {
        // Source: https://www.101computing.net/a-star-search-algorithm/

        // Arrange
        var edges = new List<Edge>
        {
            new("A", "B", 4),
            new("A", "C", 3),
            new("B", "A", 4),
            new("B", "E", 12),
            new("B", "F", 5),
            new("C", "A", 3),
            new("C", "E", 10),
            new("C", "D", 7),
            new("D", "C", 7),
            new("D", "E", 2),
            new("E", "B", 12),
            new("E", "C", 10),
            new("E", "D", 2),
            new("E", "Z", 5),
            new("F", "B", 5),
            new("F", "Z", 16),
            new("Z", "E", 5),
            new("Z", "F", 16),
        };

        var heuristics = new List<Edge>
        {
            new("A", "Z", 14),
            new("B", "Z", 12),
            new("C", "Z", 11),
            new("D", "Z", 6),
            new("E", "Z", 4),
            new("F", "Z", 11),
        };

        var aStar = new AStarSearch<string>(
            neighbors: n => edges.Where(e => e.From == n).Select(e => e.To),
            distance: (a, b) =>
            {
                return edges.Where(e => e.From == a && e.To == b).Select(e => e.Cost).FirstOrDefault(double.MaxValue);
            },
            heuristic: (a, b) =>
            {
                return heuristics.Where(e => e.From == a && e.To == b).Select(e => e.Cost).FirstOrDefault(double.MaxValue);
            }
        );

        // Act
        var path = aStar.Search("A", "Z");

        // Assert
        using (new AssertionScope())
        {
            path.Nodes.Should().BeEquivalentTo(["A", "C", "D", "E", "Z"]);
            path.TotalCost.Should().Be(17);
        }
    }

    [Fact]
    public void KnownExample2()
    {
        // Source: https://www.101computing.net/a-star-search-algorithm/

        // Arrange
        var edges = new List<Edge>
        {
            new("A", "B", 9),
            new("A", "C", 4),
            new("A", "D", 7),
            new("B", "A", 9),
            new("B", "E", 11),
            new("C", "A", 4),
            new("C", "E", 17),
            new("C", "F", 12),
            new("D", "A", 7),
            new("D", "F", 14),
            new("E", "B", 11),
            new("E", "C", 17),
            new("E", "F", 12),
            new("F", "C", 12),
            new("F", "Z", 9),
            new("Z", "E", 5),
            new("Z", "F", 9),
        };

        var heuristics = new List<Edge>
        {
            new("A", "Z", 21),
            new("B", "Z", 14),
            new("C", "Z", 18),
            new("D", "Z", 18),
            new("E", "Z", 5),
            new("F", "Z", 8),
        };

        var aStar = new AStarSearch<string>(
            neighbors: n => edges.Where(e => e.From == n).Select(e => e.To),
            distance: (a, b) =>
            {
                return edges.Where(e => e.From == a && e.To == b).Select(e => e.Cost).FirstOrDefault(double.MaxValue);
            },
            heuristic: (a, b) =>
            {
                return heuristics.Where(e => e.From == a && e.To == b).Select(e => e.Cost).FirstOrDefault(double.MaxValue);
            }
        );

        // Act
        var path = aStar.Search("A", "Z");

        // Assert
        using (new AssertionScope())
        {
            path.Nodes.Should().BeEquivalentTo(["A", "C", "F", "Z"]);
            path.TotalCost.Should().Be(25);
        }
    }

    private record Edge(string From, string To, double Cost);
}