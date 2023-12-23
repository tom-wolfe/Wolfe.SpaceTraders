using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain.Models.Marketplace;
using Wolfe.SpaceTraders.Domain.Models.Navigation;
using Wolfe.SpaceTraders.Domain.Models.Waypoints;

namespace Wolfe.SpaceTraders.Cli.Formatters;

internal static class MarketFormatter
{
    public static void WriteMarket(Market market, Waypoint waypoint, Point? relativeTo = null)
    {
        Console.WriteLine($"~ {market.Symbol.Value.Color(ConsoleColors.Id)}");

        var position = $"  Position: {waypoint.Point.ToString().Color(ConsoleColors.Point)}";
        var distance = relativeTo?.DistanceTo(waypoint.Point);
        if (distance != null) { position += $" ({distance.Total.ToString("F").Color(ConsoleColors.Distance)})"; }
        Console.WriteLine(position);

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