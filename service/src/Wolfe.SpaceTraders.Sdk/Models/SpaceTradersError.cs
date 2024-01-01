namespace Wolfe.SpaceTraders.Sdk.Models;

public class SpaceTradersError
{
    public string Message { get; set; } = "";
    public int Code { get; set; }
    public Dictionary<string, object> Data { get; set; } = [];
}