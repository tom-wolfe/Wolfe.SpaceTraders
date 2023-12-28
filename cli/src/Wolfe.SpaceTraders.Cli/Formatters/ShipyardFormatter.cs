using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Domain.Shipyards;

namespace Wolfe.SpaceTraders.Cli.Formatters;

internal static class ShipyardFormatter
{
    public static void WriteShipyard(Shipyard shipyard, Point? relativeTo = null)
    {
        Console.WriteLine($"~ {shipyard.Id.Value.Color(ConsoleColors.Id)}");

        var location = $"  Location: {shipyard.Location.ToString().Color(ConsoleColors.Point)}";
        var distance = relativeTo?.DistanceTo(shipyard.Location);
        if (distance != null) { location += $" ({distance.Total.ToString("F").Color(ConsoleColors.Distance)})"; }
        Console.WriteLine(location);

        Console.WriteLine("  Ship Types:");
        foreach (var type in shipyard.ShipTypes)
        {
            Console.WriteLine($"  - {type.Type.Value.Color(ConsoleColors.Code)}");
        }
        Console.WriteLine("  Ships:");
        foreach (var ship in shipyard.Ships)
        {
            Console.WriteLine($"  - {ship.Type.Value.Color(ConsoleColors.Code)} ({ship.PurchasePrice.ToString().Color(ConsoleColors.Currency)})");
        }
    }
}