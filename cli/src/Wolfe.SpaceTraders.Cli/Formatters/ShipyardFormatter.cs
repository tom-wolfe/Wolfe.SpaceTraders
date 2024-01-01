using Spectre.Console;
using Wolfe.SpaceTraders.Domain.General;
using Wolfe.SpaceTraders.Domain.Shipyards;

namespace Wolfe.SpaceTraders.Cli.Formatters;

internal static class ShipyardFormatter
{
    public static void WriteShipyard(Shipyard shipyard, Point? relativeTo = null)
    {
        ConsoleHelpers.WriteLineFormatted($"~ {shipyard.Id} ({shipyard.Type})");

        ConsoleHelpers.WriteFormatted($"  Location: {shipyard.Location}");
        var distance = relativeTo?.DistanceTo(shipyard.Location);
        if (distance != null) { ConsoleHelpers.WriteFormatted($" ({distance})"); }
        Console.WriteLine();

        WriteShipTypes(shipyard.ShipTypes);
    }

    private static void WriteShipTypes(IEnumerable<ShipyardShipType> types)
    {
        var table = new Table { Title = new TableTitle("Ship Types") };

        table.AddColumn("Type");

        foreach (var item in types)
        {
            table.AddRow(
                ConsoleHelpers.Renderable($"{item.Type}")
            );
        }

        AnsiConsole.Write(table);
    }
}