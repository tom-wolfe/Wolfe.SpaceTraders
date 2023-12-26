namespace Wolfe.SpaceTraders.Domain.Ships;

public class ShipFuelConsumed
{
    public required Fuel Amount { get; init; }
    public required DateTime Timestamp { get; init; }
}