namespace Wolfe.SpaceTraders.Sdk.Models.Ships;

public class SpaceTradersShipFuel
{
    public required uint Current { get; set; }
    public required uint Capacity { get; set; }
    public SpaceTradersShipFuelConsumed? Consumed { get; set; }
}