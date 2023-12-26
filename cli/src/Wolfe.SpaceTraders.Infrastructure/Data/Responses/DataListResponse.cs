namespace Wolfe.SpaceTraders.Infrastructure.Data.Responses;

internal class DataListResponse<T>
{
    public required IReadOnlyCollection<T> Items { get; init; }
    public required DateTimeOffset RetrievedAt { get; init; }

    public static readonly DataListResponse<T> None = new()
    {
        Items = Array.Empty<T>(),
        RetrievedAt = DateTimeOffset.MinValue
    };

    public bool IsValid()
    {
        return RetrievedAt != DateTimeOffset.MinValue;
    }
}