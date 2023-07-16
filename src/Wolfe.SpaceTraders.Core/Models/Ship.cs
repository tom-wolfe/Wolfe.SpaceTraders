namespace Wolfe.SpaceTraders.Core.Models;

public class Ship
{
    public required ShipSymbol Symbol { get; set; }
    public required ShipNav Nav { get; set; }
    public required ShipCrew Crew { get; set; }
    public required ShipFrame Frame { get; set; }
    public required ShipReactor Reactor { get; set; }
    public required ShipEngine Engine { get; set; }
    public required List<ShipModule> Modules { get; set; } = new();
    public required List<ShipMount> Mounts { get; set; } = new();
    public required ShipCargo Cargo { get; set; }
    public required ShipFuel Fuel { get; set; }
    public required ShipRegistration Registration { get; set; }
}