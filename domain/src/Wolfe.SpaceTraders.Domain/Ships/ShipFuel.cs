namespace Wolfe.SpaceTraders.Domain.Ships;

public class ShipFuel
{
    public required Fuel Current { get; set; }
    public required Fuel Capacity { get; set; }
    public ShipFuelConsumed? Consumed { get; set; }

    public bool IsEmpty => Current == Fuel.Zero;
    public decimal PercentRemaining => Capacity == Fuel.Zero ? 0 : Current / Capacity * 100m;

}