namespace Wolfe.SpaceTraders.Domain.General;

public struct Distance(uint x, uint y)
{
    public static Distance Zero => new(0, 0);

    public uint X => x;
    public uint Y => y;

    public double Total => Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));

    public override string ToString() => $"{X}, {Y} ({Total:F})";

    public bool Equals(object? obj) => obj is Distance distance && Equals(distance);

    private bool Equals(Distance other) => (int)other.Total == (int)Total;

    public static bool operator ==(Distance left, Distance right) => left.Equals(right);
    public static bool operator !=(Distance left, Distance right) => !(left == right);
    public static bool operator <(Distance left, Distance right) => left.Total < right.Total;
    public static bool operator <=(Distance left, Distance right) => left.Total <= right.Total;
    public static bool operator >(Distance left, Distance right) => left.Total > right.Total;
    public static bool operator >=(Distance left, Distance right) => left.Total >= right.Total;
}