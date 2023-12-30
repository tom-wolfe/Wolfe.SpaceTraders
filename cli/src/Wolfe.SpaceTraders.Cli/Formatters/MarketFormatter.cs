using Humanizer;
using Spectre.Console;
using Wolfe.SpaceTraders.Domain.Marketplaces;

namespace Wolfe.SpaceTraders.Cli.Formatters;

internal static class MarketFormatter
{
    public static void WriteMarketRanks(IEnumerable<MarketPriorityRank> ranks)
    {
        var table = new Table();

        // Add some columns
        table.AddColumn("Market");
        table.AddColumn("Location");
        table.AddColumn(new TableColumn("Distance").RightAligned());
        table.AddColumn("Age");
        table.AddColumn("Volatility");
        table.AddColumn(new TableColumn("Rank").RightAligned());

        foreach (var rank in ranks)
        {
            table.AddRow(
                rank.MarketId.ToString()!,
                rank.Location.ToString(),
                rank.Distance.ToString(),
                rank.DataAge?.Humanize() ?? "N/a",
                Math.Round(rank.Volatility, 2) + "%",
                rank.Rank.ToString("F")
            );
        }

        // Render the table to the console
        AnsiConsole.Write(table);
    }
}