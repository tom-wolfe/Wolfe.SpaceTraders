using Humanizer;
using Spectre.Console;
using Spectre.Console.Rendering;
using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Factions;
using Wolfe.SpaceTraders.Domain.General;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Cli.Formatters;

internal static class ConsoleHelpers
{
    private static readonly Style Category = new(foreground: Color.Yellow, decoration: Decoration.Bold);
    private static readonly Style Currency = new(foreground: Color.Maroon, decoration: Decoration.Bold);
    private static readonly Style Date = new(foreground: Color.HotPink3_1, decoration: Decoration.Bold);
    private static readonly Style Distance = new(foreground: Color.Magenta1, decoration: Decoration.Bold);
    private static readonly Style Fuel = new(foreground: Color.Orange1, decoration: Decoration.Bold);
    private static readonly Style Id = new(foreground: Color.Green, decoration: Decoration.Bold);
    private static readonly Style Location = new(foreground: Color.DodgerBlue1, decoration: Decoration.Bold);
    private static readonly Style Point = new(foreground: Color.DarkGreen, decoration: Decoration.Bold);
    private static readonly Style Status = new(foreground: Color.Teal, decoration: Decoration.Bold);

    private static readonly Style Success = new(foreground: Color.Lime, decoration: Decoration.Bold);

    private static readonly IFormatProvider FormatProvider = new StyledFormatProvider(
        styles: new Dictionary<Type, Style>
        {
            [typeof(ContractId)] = Id,
            [typeof(ContractType)] = Category,
            [typeof(Credits)] = Currency,
            [typeof(AccountId)] = Id,
            [typeof(AgentId)] = Id,
            [typeof(DateTimeOffset)] = Date,
            [typeof(Distance)] = Distance,
            [typeof(FactionId)] = Id,
            [typeof(Fuel)] = Fuel,
            [typeof(ItemId)] = Id,
            [typeof(MarketTradeActivity)] = Status,
            [typeof(MarketTradeType)] = Category,
            [typeof(MarketTradeSupply)] = Status,
            [typeof(Point)] = Point,
            [typeof(ShipId)] = Id,
            [typeof(ShipRole)] = Category,
            [typeof(ShipType)] = Category,
            [typeof(ShipNavigationStatus)] = Status,
            [typeof(SystemId)] = Id,
            [typeof(SystemType)] = Category,
            [typeof(TimeSpan)] = Date,
            [typeof(WaypointId)] = Location,
            [typeof(WaypointTraitId)] = Id,
            [typeof(WaypointType)] = Category,
        },
        formats: new Dictionary<(Type, string?), Func<object, string?>>
        {
            [(typeof(bool), null)] = v => ((bool)v) ? "Yes" : "No",
            [(typeof(DateTimeOffset), null)] = v => ((DateTimeOffset)v).Humanize(),
            [(typeof(TimeSpan), null)] = v => ((TimeSpan)v).Humanize(),
        }
    );

    public static IRenderable Renderable(this FormattableString value) => new Markup(value.ToString(FormatProvider));
    public static void WriteFormatted(FormattableString value) => AnsiConsole.Markup(value.ToString(FormatProvider));
    public static void WriteLineFormatted(FormattableString value) => AnsiConsole.MarkupLine(value.ToString(FormatProvider));
    public static void WriteLineSuccess(FormattableString value) => AnsiConsole.Write(new Markup(value.ToString(FormatProvider) + Environment.NewLine, Success));
}
