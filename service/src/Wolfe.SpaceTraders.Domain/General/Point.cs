namespace Wolfe.SpaceTraders.Domain.General;

public readonly struct Point(int x, int y)
{
    public static readonly Point Zero = new(0, 0);

    public int X { get; } = x;
    public int Y { get; } = y;

    public override string ToString() => $"{X}, {Y}";
    public Distance DistanceTo(Point right) => new((uint)Math.Abs(X - right.X), (uint)Math.Abs(Y - right.Y));

}