namespace Wolfe.SpaceTraders.Domain.General;

public record Point(int X, int Y)
{
    public override string ToString()
    {
        return $"{X}, {Y}";
    }

    public Distance DistanceTo(Point right)
    {
        return new Distance((uint)Math.Abs(X - right.X), (uint)Math.Abs(Y - right.Y));
    }
}