namespace Wolfe.SpaceTraders.Sdk.Models.Ships;

public class SpaceTradersShipFuel
{
    public required int Current { get; set; }
    public required int Capacity { get; set; }
    public SpaceTradersShipFuelConsumed? Consumed { get; set; }
}