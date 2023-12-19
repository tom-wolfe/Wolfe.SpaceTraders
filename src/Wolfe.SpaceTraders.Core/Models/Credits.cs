namespace Wolfe.SpaceTraders.Core.Models;

public record Credits(long Value)
{
    public static readonly Credits Zero = new(0);

    public override string ToString()
    {
        return $"\u00a4{Value:N0}";
    }

    public static Credits operator +(Credits left, Credits right) => new(left.Value + right.Value);
    public static Credits operator -(Credits left, Credits right) => new(left.Value - right.Value);
}