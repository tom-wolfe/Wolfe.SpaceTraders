using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain.Marketplace;
using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Cli.Formatters;

internal static class MarketplaceFormatter
{
    public static void WriteMarketplace(Marketplace marketplace, Waypoint waypoint, Point? relativeTo = null)
    {
        Console.WriteLine($"~ {marketplace.Symbol.Value.Color(ConsoleColors.Id)}");

        var location = $"  Location: {waypoint.Location.ToString().Color(ConsoleColors.Point)}";
        var distance = relativeTo?.DistanceTo(waypoint.Location);
        if (distance != null) { location += $" ({distance.Total.ToString("F").Color(ConsoleColors.Distance)})"; }
        Console.WriteLine(location);

        Console.WriteLine("  Imports:");
        foreach (var item in marketplace.Imports)
        {
            Console.WriteLine($"  - {item.Name.Color(ConsoleColors.Information)} ({item.Symbol.Value.Color(ConsoleColors.Code)})");
        }

        Console.WriteLine("  Exports:");
        foreach (var item in marketplace.Exports)
        {
            Console.WriteLine($"  - {item.Name.Color(ConsoleColors.Information)} ({item.Symbol.Value.Color(ConsoleColors.Code)})");
        }

        Console.WriteLine("  Exchange:");
        foreach (var item in marketplace.Exchange)
        {
            Console.WriteLine($"  - {item.Name.Color(ConsoleColors.Information)} ({item.Symbol.Value.Color(ConsoleColors.Code)})");
        }
    }
}