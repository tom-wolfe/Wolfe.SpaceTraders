namespace Wolfe.SpaceTraders.Core.Models;

public class ShipyardShip
{
    public required ShipType Type { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required int PurchasePrice { get; set; }
    public required ShipFrame Frame { get; set; }
    public required ShipReactor Reactor { get; set; }
    public required ShipEngine Engine { get; set; }
    public required List<ShipModule> Modules { get; set; } = new();
    public required List<ShipMount> Mounts { get; set; } = new();
}