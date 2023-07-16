namespace Wolfe.SpaceTraders.Core.Models;

public class ShipFuel
{
    public required int Current { get; set; }
    public required int Capacity { get; set; }
    public ShipFuelConsumed? Consumed { get; set; }
}