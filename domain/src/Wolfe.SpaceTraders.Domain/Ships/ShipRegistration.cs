namespace Wolfe.SpaceTraders.Domain.Ships;

public class ShipRegistration
{
    public required string Name { get; init; }
    public required FactionId FactionId { get; init; }
    public required ShipRole Role { get; init; }
}