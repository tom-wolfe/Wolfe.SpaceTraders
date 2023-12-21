namespace Wolfe.SpaceTraders.Domain.Models;

public record Distance(uint X, uint Y)
{
    public double Total => Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));

    public override string ToString()
    {
        return $"{X}, {Y}";
    }
}