namespace Wolfe.SpaceTraders.Domain.Models;

public record Location(int X, int Y)
{
    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}