namespace Wolfe.SpaceTraders.Domain.General;

public readonly struct Point(int x, int y)
{
    public int X { get; } = x;
    public int Y { get; } = y;

    public override string ToString() => $"{X}, {Y}";
    public Distance DistanceTo(Point right) => new((uint)Math.Abs(X - right.X), (uint)Math.Abs(Y - right.Y));

}