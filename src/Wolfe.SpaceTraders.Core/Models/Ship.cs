namespace Wolfe.SpaceTraders.Core.Models;

public class Ship
{
    public required ShipSymbol Symbol { get; set; }
    public required ShipRegistration Registration { get; set; }
    public required Navigation Navigation { get; set; }
    public required ShipFuel Fuel { get; set; }
}