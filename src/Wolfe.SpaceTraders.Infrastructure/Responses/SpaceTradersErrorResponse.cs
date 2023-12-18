namespace Wolfe.SpaceTraders.Infrastructure.Responses;

internal class SpaceTradersErrorResponse
{
    public SpaceTradersError Error { get; set; } = new();
}

internal class SpaceTradersError
{
    public string Message { get; set; } = "";
    public int Code { get; set; }
    public Dictionary<string, object> Data { get; set; } = new();
}