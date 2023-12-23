namespace Wolfe.SpaceTraders.Sdk.Requests;

public class SpaceTradersShipSellRequest
{
    public required string Symbol { get; set; }
    public required int Units { get; set; }
}