namespace Wolfe.SpaceTraders.Infrastructure.Data.Models;

internal class DataItem<T>
{
    public DateTimeOffset RetrievedAt { get; init; }
    public required T Item { get; init; }
}
