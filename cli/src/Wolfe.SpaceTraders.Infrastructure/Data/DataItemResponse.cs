namespace Wolfe.SpaceTraders.Infrastructure.Data;

internal class DataItemResponse<T>
{
    public required T Item { get; init; }
    public required DateTimeOffset RetrievedAt { get; init; }
}