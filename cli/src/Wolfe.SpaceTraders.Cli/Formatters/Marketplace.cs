using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Navigation;

namespace Wolfe.SpaceTraders.Cli.Formatters;

internal static class MarketplaceFormatter
{
    public static void WriteMarketplace(Marketplace marketplace, Point? relativeTo = null)
    {
        Console.WriteLine($"~ {marketplace.Id.Value.Color(ConsoleColors.Id)}");

        var location = $"  Location: {marketplace.Location.ToString().Color(ConsoleColors.Point)}";
        var distance = relativeTo?.DistanceTo(marketplace.Location);
        if (distance != null) { location += $" ({distance.Total.ToString("F").Color(ConsoleColors.Distance)})"; }
        Console.WriteLine(location);

        Console.WriteLine("  Imports:");
        foreach (var item in marketplace.Imports)
        {
            Console.WriteLine($"  - {item.Name.Color(ConsoleColors.Information)} ({item.ItemId.Value.Color(ConsoleColors.Code)})");
        }

        Console.WriteLine("  Exports:");
        foreach (var item in marketplace.Exports)
        {
            Console.WriteLine($"  - {item.Name.Color(ConsoleColors.Information)} ({item.ItemId.Value.Color(ConsoleColors.Code)})");
        }

        Console.WriteLine("  Exchange:");
        foreach (var item in marketplace.Exchange)
        {
            Console.WriteLine($"  - {item.Name.Color(ConsoleColors.Information)} ({item.ItemId.Value.Color(ConsoleColors.Code)})");
        }
    }
}