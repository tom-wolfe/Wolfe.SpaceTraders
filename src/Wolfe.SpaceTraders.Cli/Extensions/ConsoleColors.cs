namespace Wolfe.SpaceTraders.Extensions;

internal record ConsoleColors(ConsoleColor? Foreground, ConsoleColor? Background)
{
    public static readonly ConsoleColors Default = new(ConsoleColor.Gray, ConsoleColor.Black);
    public static readonly ConsoleColors Information = new(ConsoleColor.DarkGreen, ConsoleColor.Black);
    public static readonly ConsoleColors Warning = new(ConsoleColor.Yellow, ConsoleColor.DarkRed);
    public static readonly ConsoleColors Critical = new(ConsoleColor.White, ConsoleColor.DarkRed);
    public static readonly ConsoleColors Error = new(ConsoleColor.Black, ConsoleColor.DarkRed);
};