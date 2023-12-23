namespace Wolfe.SpaceTraders.Cli.Extensions;

internal static class BooleanExtensions
{
    public static string Humanize(this bool value, string trueString = "Yes", string falseString = "No") => value ? trueString : falseString;
}