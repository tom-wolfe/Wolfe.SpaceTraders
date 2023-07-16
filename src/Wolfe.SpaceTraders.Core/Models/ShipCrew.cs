namespace Wolfe.SpaceTraders.Core.Models;

public class ShipCrew
{
    public required int Current { get; set; }
    public required int Required { get; set; }
    public required int Capacity { get; set; }
    public required ShipCrewRotation Rotation { get; set; }
    public required int Morale { get; set; }
    public required int Wages { get; set; }
}