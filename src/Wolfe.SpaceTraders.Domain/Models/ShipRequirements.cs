namespace Wolfe.SpaceTraders.Domain.Models;

public class ShipRequirements
{
    public int Power { get; set; }
    public required int Crew { get; set; }
    public int Slots { get; set; }
}