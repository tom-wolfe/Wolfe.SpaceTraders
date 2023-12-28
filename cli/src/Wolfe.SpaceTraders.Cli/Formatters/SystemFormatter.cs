using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain.Systems;

namespace Wolfe.SpaceTraders.Cli.Formatters;

internal static class SystemFormatter
{
    public static void WriteSystem(StarSystem system)
    {
        Console.WriteLine($"~ {system.Id.Value.Color(ConsoleColors.Id)}");
        Console.WriteLine($"  Sector: {system.SectorId.Value.Color(ConsoleColors.Id)}");
        Console.WriteLine($"  Type: {system.Type.Value.Color(ConsoleColors.Code)}");
        Console.WriteLine($"  Location: {system.Location}");
    }
}