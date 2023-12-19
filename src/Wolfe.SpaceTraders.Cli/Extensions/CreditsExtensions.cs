using Wolfe.SpaceTraders.Core.Models;

namespace Wolfe.SpaceTraders.Cli.Extensions;

internal static class CreditsExtensions
{
    public static string Currency(this Credits value) => $"\u00a4{value:N0}".Color(ConsoleColors.Currency);
}