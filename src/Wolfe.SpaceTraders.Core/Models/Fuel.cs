namespace Wolfe.SpaceTraders.Core.Models;

public record Fuel(int Value)
{
    public static readonly Fuel Zero = new(0);
}