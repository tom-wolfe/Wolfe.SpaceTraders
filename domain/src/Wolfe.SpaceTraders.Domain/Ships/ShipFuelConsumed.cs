namespace Wolfe.SpaceTraders.Domain.Ships;

public class ShipFuelConsumed
{
    public required Fuel Amount { get; set; }
    public required DateTime Timestamp { get; set; }
}