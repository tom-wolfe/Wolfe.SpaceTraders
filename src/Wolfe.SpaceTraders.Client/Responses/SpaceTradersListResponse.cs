namespace Wolfe.SpaceTraders.Responses;

public class SpaceTradersListResponse<T>
{
    public required IEnumerable<T> Data { get; set; }
    public required ListResponseMeta Meta { get; set; }
}