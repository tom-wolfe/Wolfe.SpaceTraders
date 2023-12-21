namespace Wolfe.SpaceTraders.Sdk.Models.Ships;

public class SpaceTradersShipCargo
{
    public required int Capacity { get; set; }
    public required int Units { get; set; }
    public required IReadOnlyCollection<SpaceTradersCargoItem> Inventory { get; set; }
}