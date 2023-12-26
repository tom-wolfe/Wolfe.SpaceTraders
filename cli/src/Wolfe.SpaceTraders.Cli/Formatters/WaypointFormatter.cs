using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Cli.Formatters;

internal static class WaypointFormatter
{
    public static void WriteWaypoint(Waypoint waypoint, Point? relativeTo = null)
    {
        Console.WriteLine($"~ {waypoint.Symbol.Value.Color(ConsoleColors.Id)} ({waypoint.Type.Value.Color(ConsoleColors.Category)})");

        var location = $"  Location: {waypoint.Location.ToString().Color(ConsoleColors.Point)}";
        var distance = relativeTo?.DistanceTo(waypoint.Location);
        if (distance != null) { location += $" ({distance.Total.ToString("F").Color(ConsoleColors.Distance)})"; }
        Console.WriteLine(location);

        Console.WriteLine("  Traits:");
        foreach (var trait in waypoint.Traits)
        {
            Console.WriteLine($"  - {trait.Name.Color(ConsoleColors.Information)} ({trait.Symbol.Value.Color(ConsoleColors.Code)})");
        }
    }
}