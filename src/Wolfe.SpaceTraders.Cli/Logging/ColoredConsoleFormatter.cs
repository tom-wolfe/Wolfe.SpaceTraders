using System.Globalization;
using Microsoft.Extensions.Logging.Console;
using Wolfe.SpaceTraders.Cli.Extensions;

namespace Wolfe.SpaceTraders.Cli.Logging;

internal class ColoredConsoleFormatter : ConsoleFormatter
{
    public ColoredConsoleFormatter() : base(nameof(ColoredConsoleFormatter)) { }

    public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider? scopeProvider, TextWriter textWriter)
    {
        var message = logEntry.Formatter.Invoke(logEntry.State, logEntry.Exception);

        textWriter.Write(DateTime.UtcNow.ToString("hh:mm:ss.fff", CultureInfo.CurrentCulture));
        textWriter.Write(" ");
        textWriter.Write($"[{logEntry.LogLevel}]".Color(GetLogLevelConsoleColors(logEntry.LogLevel)));
        if (!string.IsNullOrWhiteSpace(logEntry.Category))
        {
            textWriter.Write(" ");
            textWriter.Write($"[{logEntry.Category.Split('.').Last().Color(ConsoleColors.Category)}]");
        }
        textWriter.Write(" ");
        textWriter.Write($"{message}");
        textWriter.Write(Environment.NewLine);
    }

    private static ConsoleColors GetLogLevelConsoleColors(LogLevel logLevel) =>
        logLevel switch
        {
            LogLevel.Trace => ConsoleColors.Default,
            LogLevel.Debug => ConsoleColors.Default,
            LogLevel.Information => ConsoleColors.Information,
            LogLevel.Warning => ConsoleColors.Warning,
            LogLevel.Error => ConsoleColors.Error,
            LogLevel.Critical => ConsoleColors.Critical,
            _ => new ConsoleColors(null, null)
        };
}