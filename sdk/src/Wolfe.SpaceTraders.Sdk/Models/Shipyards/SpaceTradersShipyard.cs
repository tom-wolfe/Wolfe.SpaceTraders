namespace Wolfe.SpaceTraders.Sdk.Models.Shipyards;

public class SpaceTradersShipyard
{
    public required string Symbol { get; set; }
    public required IReadOnlyCollection<SpaceTradersShipyardShipType> ShipTypes { get; set; }
    public required IReadOnlyCollection<SpaceTradersShipyardTransaction> Transactions { get; set; }
    public required IReadOnlyCollection<SpaceTradersShipyardShip> Ships { get; set; }
}