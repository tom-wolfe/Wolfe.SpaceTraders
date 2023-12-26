namespace Wolfe.SpaceTraders.Domain.Ships;

public class ShipFuel
{
    public required Fuel Current { get; init; }
    public required Fuel Capacity { get; init; }
    public ShipFuelConsumed? Consumed { get; init; }

    public bool IsEmpty => Current == Fuel.Zero;
    public decimal PercentRemaining => Capacity == Fuel.Zero ? 0 : Current / Capacity * 100m;

}