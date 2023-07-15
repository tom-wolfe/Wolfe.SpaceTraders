namespace Wolfe.SpaceTraders.Core.Models;

public class ShipFrame
{
    public required ShipFrameSymbol Symbol { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int Condition { get; set; }
    public required int ModuleSlots { get; set; }
    public required int MountingPoints { get; set; }
    public required int FuelCapacity { get; set; }
    public required ShipRequirements Requirements { get; set; }
}