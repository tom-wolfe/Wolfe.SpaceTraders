using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain.Marketplace;
using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Cli.Formatters;

internal static class MarketFormatter
{
    public static void WriteMarket(Market market, Waypoint waypoint, Point? relativeTo = null)
    {
        Console.WriteLine($"~ {market.Symbol.Value.Color(ConsoleColors.Id)}");

        var location = $"  Location: {waypoint.Location.ToString().Color(ConsoleColors.Point)}";
        var distance = relativeTo?.DistanceTo(waypoint.Location);
        if (distance != null) { location += $" ({distance.Total.ToString("F").Color(ConsoleColors.Distance)})"; }
        Console.WriteLine(location);

        Console.WriteLine("  Imports:");
        foreach (var item in market.Imports)
        {
            Console.WriteLine($"  - {item.Name.Color(ConsoleColors.Information)} ({item.Symbol.Value.Color(ConsoleColors.Code)})");
        }

        Console.WriteLine("  Exports:");
        foreach (var item in market.Exports)
        {
            Console.WriteLine($"  - {item.Name.Color(ConsoleColors.Information)} ({item.Symbol.Value.Color(ConsoleColors.Code)})");
        }

        Console.WriteLine("  Exchange:");
        foreach (var item in market.Exchange)
        {
            Console.WriteLine($"  - {item.Name.Color(ConsoleColors.Information)} ({item.Symbol.Value.Color(ConsoleColors.Code)})");
        }
    }
}