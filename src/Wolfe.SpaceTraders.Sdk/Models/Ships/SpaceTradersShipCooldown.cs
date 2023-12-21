namespace Wolfe.SpaceTraders.Sdk.Models.Ships;

public class SpaceTradersShipCooldown
{
    public required string ShipSymbol { get; set; }
    public required int TotalSeconds { get; set; }
    public required int RemainingSeconds { get; set; }
    public required DateTime Expiration { get; set; }
}