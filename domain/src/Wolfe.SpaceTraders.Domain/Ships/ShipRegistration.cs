namespace Wolfe.SpaceTraders.Domain.Ships;

public class ShipRegistration
{
    public required string Name { get; init; }
    public required FactionSymbol FactionSymbol { get; init; }
    public required ShipRole Role { get; init; }
}