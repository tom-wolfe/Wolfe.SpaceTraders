namespace Wolfe.SpaceTraders.Sdk.Models;

public class SpaceTradersFaction
{
    public required string Symbol { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Headquarters { get; set; }
    public bool IsRecruiting { get; set; }
}