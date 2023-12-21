namespace Wolfe.SpaceTraders.Domain.Models.Ships;

public class Ship
{
    public required ShipSymbol Symbol { get; set; }
    public required ShipRegistration Registration { get; set; }
    public required Navigation.Navigation Navigation { get; set; }
    public required ShipFuel Fuel { get; set; }
    public required ShipCargo Cargo { get; set; }
}