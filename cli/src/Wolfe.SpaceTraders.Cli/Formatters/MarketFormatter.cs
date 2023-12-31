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
        table.AddColumn(new TableColumn("Location").RightAligned());
        table.AddColumn(new TableColumn("Distance").RightAligned());
        table.AddColumn("Age");
        table.AddColumn(new TableColumn("Volatility").RightAligned());
        table.AddColumn(new TableColumn("Rank").RightAligned());

        foreach (var rank in ranks)
        {
            table.AddRow(
                ConsoleHelpers.Renderable($"{rank.MarketId}"),
                ConsoleHelpers.Renderable($"{rank.Location}"),
                ConsoleHelpers.Renderable($"{rank.Distance}"),
                ConsoleHelpers.Renderable($"{rank.DataAge}"),
                ConsoleHelpers.Renderable($"{rank.Volatility:N}"),
                ConsoleHelpers.Renderable($"{rank.Rank:N}")
            );
        }

        // Render the table to the console
        AnsiConsole.Write(table);
    }

    public static void WriteMarketData(MarketData data)
    {
        ConsoleHelpers.WriteLineFormatted($"~ {data.WaypointId}");
        ConsoleHelpers.WriteLineFormatted($"  Age: {data.Age.Humanize()}");

        var tradeTable = new Table { Title = new TableTitle("Trade Goods") };

        tradeTable.AddColumn("Item");
        tradeTable.AddColumn("Type");
        tradeTable.AddColumn("Activity");
        tradeTable.AddColumn("Supply");
        tradeTable.AddColumn(new TableColumn("Sell Price").RightAligned());
        tradeTable.AddColumn(new TableColumn("Purchase Price").RightAligned());
        tradeTable.AddColumn(new TableColumn("Volume").RightAligned());

        foreach (var good in data.TradeGoods)
        {
            tradeTable.AddRow(
                ConsoleHelpers.Renderable($"{good.ItemId}"),
                ConsoleHelpers.Renderable($"{good.Type}"),
                ConsoleHelpers.Renderable($"{good.Activity}"),
                ConsoleHelpers.Renderable($"{good.Supply}"),
                ConsoleHelpers.Renderable($"{good.SellPrice}"),
                ConsoleHelpers.Renderable($"{good.PurchasePrice}"),
                ConsoleHelpers.Renderable($"{good.Volume}")
            );
        }
        AnsiConsole.Write(tradeTable);

        Console.WriteLine("  Transactions:");
    }
}