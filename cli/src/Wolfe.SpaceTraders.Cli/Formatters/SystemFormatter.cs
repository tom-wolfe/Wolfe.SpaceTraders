using Wolfe.SpaceTraders.Domain.Exploration;

namespace Wolfe.SpaceTraders.Cli.Formatters;

internal static class SystemFormatter
{
    public static void WriteSystem(StarSystem system)
    {
        ConsoleHelpers.WriteLineFormatted($"~ {system.Id}");
        ConsoleHelpers.WriteLineFormatted($"  Sector: {system.SectorId}");
        ConsoleHelpers.WriteLineFormatted($"  Type: {system.Type}");
        ConsoleHelpers.WriteLineFormatted($"  Location: {system.Location}");
    }
}