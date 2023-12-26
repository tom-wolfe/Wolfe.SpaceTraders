namespace Wolfe.SpaceTraders.Infrastructure.Data.Models;

internal class DataList<T>
{
    public DateTimeOffset RetrievedAt { get; init; }
    public required IReadOnlyCollection<T> Items { get; init; }
}
