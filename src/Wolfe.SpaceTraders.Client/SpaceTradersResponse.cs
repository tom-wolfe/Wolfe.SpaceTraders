namespace Wolfe.SpaceTraders.Infrastructure;

public class SpaceTradersResponse<T>
{
    public required T Data { get; set; }
}
