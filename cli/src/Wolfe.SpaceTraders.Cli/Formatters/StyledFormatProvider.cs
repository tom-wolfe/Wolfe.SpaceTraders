using Spectre.Console;

namespace Wolfe.SpaceTraders.Cli.Formatters;

internal class StyledFormatProvider(
    IReadOnlyDictionary<Type, Style> styles,
    IReadOnlyDictionary<(Type, string?), Func<object, string?>> formats
) : IFormatProvider, ICustomFormatter
{
    public object? GetFormat(Type? formatType) => formatType == typeof(ICustomFormatter) ? this : null;

    public string Format(string? format, object? arg, IFormatProvider? formatProvider)
    {
        if (arg == null) { return string.Empty; }

        var argType = arg.GetType();

        string? formattedArg;
        if (formats.TryGetValue((argType, format), out var formatter))
        {
            formattedArg = formatter(arg) ?? string.Empty;
        }
        else
        {
            if (arg is IFormattable formattable)
            {
                formattedArg = formattable.ToString(format, null);
            }
            else
            {
                formattedArg = arg.ToString() ?? string.Empty;
            }
        }

        if (!styles.TryGetValue(argType, out var style))
        {
            return formattedArg;
        }
        var markup = style.ToMarkup();

        return $"[{markup}]{formattedArg}[/]";
    }
}
