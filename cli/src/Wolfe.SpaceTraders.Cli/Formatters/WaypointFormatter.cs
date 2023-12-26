using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Cli.Formatters;

internal static class WaypointFormatter
{
    public static void WriteWaypoint(Waypoint waypoint, Point? relativeTo = null)
    {
        Console.WriteLine($"~ {waypoint.Symbol.Value.Color(ConsoleColors.Id)} ({waypoint.Type.Value.Color(ConsoleColors.Category)})");

        var position = $"  Position: {waypoint.Point.ToString().Color(ConsoleColors.Point)}";
        var distance = relativeTo?.DistanceTo(waypoint.Point);
        if (distance != null) { position += $" ({distance.Total.ToString("F").Color(ConsoleColors.Distance)})"; }
        Console.WriteLine(position);

        Console.WriteLine("  Traits:");
        foreach (var trait in waypoint.Traits)
        {
            Console.WriteLine($"  - {trait.Name.Color(ConsoleColors.Information)} ({trait.Symbol.Value.Color(ConsoleColors.Code)})");
        }
    }
}