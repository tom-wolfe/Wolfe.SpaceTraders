namespace Wolfe.SpaceTraders.Sdk.Models.Shipyards;

public class SpaceTradersShipyard
{
    public required string Symbol { get; set; }
    public required IReadOnlyCollection<SpaceTradersShipyardShipType> ShipTypes { get; set; }
    public IReadOnlyCollection<SpaceTradersShipyardTransaction>? Transactions { get; set; }
    public IReadOnlyCollection<SpaceTradersShipyardShip>? Ships { get; set; }
}