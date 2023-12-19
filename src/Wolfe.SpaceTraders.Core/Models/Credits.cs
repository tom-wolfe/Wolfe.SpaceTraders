namespace Wolfe.SpaceTraders.Core.Models;

public record Credits(long Value)
{
    public static readonly Credits Zero = new(0);
}