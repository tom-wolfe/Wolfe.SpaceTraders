namespace Wolfe.SpaceTraders.Cli.Extensions;

internal record ConsoleColors(ConsoleColor? Foreground, ConsoleColor? Background)
{
    public static readonly ConsoleColors Default = new(ConsoleColor.Gray, ConsoleColor.Black);
    public static readonly ConsoleColors Success = new(ConsoleColor.DarkGreen, ConsoleColor.Black);
    public static readonly ConsoleColors Warning = new(ConsoleColor.Yellow, ConsoleColor.DarkRed);
    public static readonly ConsoleColors Critical = new(ConsoleColor.White, ConsoleColor.DarkRed);
    public static readonly ConsoleColors Error = new(ConsoleColor.Black, ConsoleColor.DarkRed);
    public static readonly ConsoleColors Category = new(ConsoleColor.Cyan, ConsoleColor.Black);

    public static readonly ConsoleColors Information = new(ConsoleColor.DarkCyan, ConsoleColor.Black);
    public static readonly ConsoleColors Id = new(ConsoleColor.DarkBlue, ConsoleColor.Black);
    public static readonly ConsoleColors Code = new(ConsoleColor.Blue, ConsoleColor.Black);
    public static readonly ConsoleColors Status = new(ConsoleColor.Yellow, ConsoleColor.Black);
    public static readonly ConsoleColors Currency = new(ConsoleColor.DarkRed, ConsoleColor.Black);
    public static readonly ConsoleColors Distance = new(ConsoleColor.Magenta, ConsoleColor.Black);
    public static readonly ConsoleColors Fuel = new(ConsoleColor.DarkYellow, ConsoleColor.Black);
    public static readonly ConsoleColors Point = new(ConsoleColor.DarkGreen, ConsoleColor.Black);
};