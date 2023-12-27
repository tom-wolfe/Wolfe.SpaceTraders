namespace Wolfe.SpaceTraders.Domain.Ships;

public class Ship
{
    public required ShipId Id { get; init; }
    public required ShipRegistration Registration { get; init; }
    public required Navigation.Navigation Navigation { get; init; }
    public required ShipFuel Fuel { get; init; }
    public required ShipCargo Cargo { get; init; }
}