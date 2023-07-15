namespace Wolfe.SpaceTraders.Core.Models;

public class ShipRequirements
{
    public int Power { get; set; }
    public required int Crew { get; set; }
    public int Slots { get; set; }
}