﻿namespace Wolfe.SpaceTraders.Cli.Extensions;

internal static class StringExtensions
{
    private const string DefaultForegroundColor = "\x1B[39m\x1B[22m"; // reset to default foreground color
    private const string DefaultBackgroundColor = "\x1B[49m"; // reset to the background color

    public static string Color(this string input, ConsoleColors colors) =>
        $"{GetBackgroundColorEscapeCode(colors.Background)}{GetForegroundColorEscapeCode(colors.Foreground)}{input}{DefaultForegroundColor}{DefaultBackgroundColor}";

    public static string Heading(this string input) =>
        $"=== {input.ToUpper()} ===";

    private static string GetForegroundColorEscapeCode(ConsoleColor? color) =>
        color switch
        {
            ConsoleColor.Black => "\x1B[30m",
            ConsoleColor.DarkRed => "\x1B[31m",
            ConsoleColor.DarkGreen => "\x1B[32m",
            ConsoleColor.DarkYellow => "\x1B[33m",
            ConsoleColor.DarkBlue => "\x1B[34m",
            ConsoleColor.DarkMagenta => "\x1B[35m",
            ConsoleColor.DarkCyan => "\x1B[36m",
            ConsoleColor.Gray => "\x1B[37m",
            ConsoleColor.Red => "\x1B[1m\x1B[31m",
            ConsoleColor.Green => "\x1B[1m\x1B[32m",
            ConsoleColor.Yellow => "\x1B[1m\x1B[33m",
            ConsoleColor.Blue => "\x1B[1m\x1B[34m",
            ConsoleColor.Magenta => "\x1B[1m\x1B[35m",
            ConsoleColor.Cyan => "\x1B[1m\x1B[36m",
            ConsoleColor.White => "\x1B[1m\x1B[37m",
            _ => DefaultForegroundColor // default foreground color
        };

    private static string GetBackgroundColorEscapeCode(ConsoleColor? color) =>
        color switch
        {
            ConsoleColor.Black => "\x1B[40m",
            ConsoleColor.DarkRed => "\x1B[41m",
            ConsoleColor.DarkGreen => "\x1B[42m",
            ConsoleColor.DarkYellow => "\x1B[43m",
            ConsoleColor.DarkBlue => "\x1B[44m",
            ConsoleColor.DarkMagenta => "\x1B[45m",
            ConsoleColor.DarkCyan => "\x1B[46m",
            ConsoleColor.Gray => "\x1B[47m",
            _ => DefaultBackgroundColor // Use default background color
        };
}