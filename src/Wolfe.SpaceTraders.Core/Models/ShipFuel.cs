namespace Wolfe.SpaceTraders.Core.Models;

public class ShipFuel
{
    public required Fuel Current { get; set; }
    public required Fuel Capacity { get; set; }
    public ShipFuelConsumed? Consumed { get; set; }
}