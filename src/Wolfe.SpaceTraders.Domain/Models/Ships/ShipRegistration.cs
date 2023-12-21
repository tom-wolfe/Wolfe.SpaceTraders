namespace Wolfe.SpaceTraders.Domain.Models.Ships;

public class ShipRegistration
{
    public required string Name { get; set; }
    public required FactionSymbol FactionSymbol { get; set; }
    public required ShipRole Role { get; set; }
}