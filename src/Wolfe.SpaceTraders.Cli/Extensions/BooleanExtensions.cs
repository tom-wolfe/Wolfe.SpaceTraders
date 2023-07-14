namespace Wolfe.SpaceTraders.Extensions;

internal static class BooleanExtensions
{
    public static string Humanize(this bool value, string trueString = "Yes", string falseString = "No") => value ? trueString : falseString;
}