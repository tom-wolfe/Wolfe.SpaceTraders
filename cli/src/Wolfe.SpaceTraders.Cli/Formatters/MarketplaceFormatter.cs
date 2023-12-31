using Spectre.Console;
using Wolfe.SpaceTraders.Domain.General;
using Wolfe.SpaceTraders.Domain.Marketplaces;

namespace Wolfe.SpaceTraders.Cli.Formatters;

internal static class MarketplaceFormatter
{
    public static void WriteMarketplace(Marketplace marketplace, Point? relativeTo = null)
    {
        ConsoleHelpers.WriteLineFormatted($"~ {marketplace.Id} ({marketplace.Type})");

        ConsoleHelpers.WriteFormatted($"  Location: {marketplace.Location}");
        var distance = relativeTo?.DistanceTo(marketplace.Location);
        if (distance != null) { ConsoleHelpers.WriteFormatted($" ({distance})"); }
        Console.WriteLine();

        WriteMarketPlaceItems("Imports", marketplace.Imports);
        WriteMarketPlaceItems("Export", marketplace.Exports);
        WriteMarketPlaceItems("Exchange", marketplace.Exchange);
    }

    private static void WriteMarketPlaceItems(string title, IEnumerable<MarketplaceItem> items)
    {
        var table = new Table { Title = new TableTitle(title) };

        table.AddColumn("Item");
        table.AddColumn("Name");
        table.AddColumn("Description");

        foreach (var item in items)
        {
            table.AddRow(
                ConsoleHelpers.Renderable($"{item.ItemId}"),
                ConsoleHelpers.Renderable($"{item.Name}"),
                ConsoleHelpers.Renderable($"{item.Description}")
            );
        }

        AnsiConsole.Write(table);
    }
}