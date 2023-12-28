
namespace Wolfe.SpaceTraders.Sdk.Requests;

public class SpaceTradersShipJettisonRequest
{
    public required string Symbol { get; set; }
    public required int Units { get; set; }
}