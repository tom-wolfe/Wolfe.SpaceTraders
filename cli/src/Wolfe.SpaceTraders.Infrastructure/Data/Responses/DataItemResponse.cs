namespace Wolfe.SpaceTraders.Infrastructure.Data.Responses;

internal class DataItemResponse<T>
{
    public required T? Item { get; init; }
    public required DateTimeOffset RetrievedAt { get; init; }

    public static readonly DataItemResponse<T> None = new()
    {
        Item = default,
        RetrievedAt = DateTimeOffset.MinValue
    };

    public bool IsValid()
    {
        return RetrievedAt != DateTimeOffset.MinValue && Item != null;
    }
}