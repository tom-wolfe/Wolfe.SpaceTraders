using Spectre.Console;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.General;

namespace Wolfe.SpaceTraders.Cli.Formatters;

internal static class WaypointFormatter
{
    public static void WriteWaypoint(Waypoint waypoint, Point? relativeTo = null)
    {
        ConsoleHelpers.WriteLineFormatted($"~ {waypoint.Id} ({waypoint.Type})");

        ConsoleHelpers.WriteFormatted($"  Location: {waypoint.Location}");
        var distance = relativeTo?.DistanceTo(waypoint.Location);
        if (distance != null) { ConsoleHelpers.WriteFormatted($" ({distance})"); }
        Console.WriteLine();

        WriteWaypointTraits(waypoint.Traits);
    }

    private static void WriteWaypointTraits(IEnumerable<WaypointTrait> traits)
    {
        var table = new Table { Title = new TableTitle("Traits") };

        table.AddColumn("Trait");
        table.AddColumn("Name");
        table.AddColumn("Description");

        foreach (var trait in traits)
        {
            table.AddRow(
                ConsoleHelpers.Renderable($"{trait.Id}"),
                ConsoleHelpers.Renderable($"{trait.Name}"),
                ConsoleHelpers.Renderable($"{trait.Description}")
            );
        }

        AnsiConsole.Write(table);
    }
}