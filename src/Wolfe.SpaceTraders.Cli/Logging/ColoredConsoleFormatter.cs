
using Microsoft.Extensions.Logging.Console;
using System.Globalization;
using Wolfe.SpaceTraders.Extensions;

namespace Wolfe.SpaceTraders.Logging;

internal class ColoredConsoleFormatter : ConsoleFormatter
{
    public ColoredConsoleFormatter() : base(nameof(ColoredConsoleFormatter)) { }

    public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider? scopeProvider, TextWriter textWriter)
    {
        var message = logEntry.Formatter?.Invoke(logEntry.State, logEntry.Exception);
        if (message is null)
        { return; }

        textWriter.Write(DateTime.UtcNow.ToString("hh:mm:ss.fff", CultureInfo.CurrentCulture));
        textWriter.Write(" ");
        textWriter.WriteColored($"[{logEntry.LogLevel}]", GetLogLevelConsoleColors(logEntry.LogLevel));
        if (!string.IsNullOrWhiteSpace(logEntry.Category))
        {
            textWriter.Write(" ");
            textWriter.WriteColored($"[{logEntry.Category.Split('.').Last()}]", new ConsoleColors(ConsoleColor.Cyan, ConsoleColor.Black));
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