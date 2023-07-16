namespace Wolfe.SpaceTraders.Core.Models;

public class ShipRegistration
{
    public required string Name { get; set; }
    public required FactionSymbol FactionSymbol { get; set; }
    public required ShipRole Role { get; set; }
}