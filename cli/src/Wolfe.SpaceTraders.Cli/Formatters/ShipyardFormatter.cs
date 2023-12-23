using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain.Models.Shipyards;

namespace Wolfe.SpaceTraders.Cli.Formatters;

internal static class ShipyardFormatter
{
    public static void WriteShipyard(Shipyard shipyard)
    {
        Console.WriteLine($"~ {shipyard.Symbol.Value.Color(ConsoleColors.Id)}");

        Console.WriteLine("  Ships:");
        foreach (var ship in shipyard.Ships)
        {
            Console.WriteLine($"  - {ship.Type.Value.Color(ConsoleColors.Code)} ({ship.PurchasePrice})");
        }
    }
}