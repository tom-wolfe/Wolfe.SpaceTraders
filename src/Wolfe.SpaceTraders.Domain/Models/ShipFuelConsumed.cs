namespace Wolfe.SpaceTraders.Domain.Models;

public class ShipFuelConsumed
{
    public required Fuel Amount { get; set; }
    public required DateTime Timestamp { get; set; }
}