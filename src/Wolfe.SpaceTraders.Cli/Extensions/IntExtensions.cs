namespace Wolfe.SpaceTraders.Cli.Extensions;

internal static class IntExtensions
{
    public static string Currency(this int value) => $"\u00a4{value:N0}".Color(ConsoleColors.Currency);
}