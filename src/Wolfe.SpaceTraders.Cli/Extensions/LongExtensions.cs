namespace Wolfe.SpaceTraders.Cli.Extensions;

internal static class LongExtensions
{
    public static string Currency(this long value) => $"\u00a4{value:N0}".Color(ConsoleColors.Currency);
}