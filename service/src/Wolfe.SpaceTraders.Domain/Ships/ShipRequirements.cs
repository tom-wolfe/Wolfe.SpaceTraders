namespace Wolfe.SpaceTraders.Domain.Ships;

public class ShipRequirements
{
    public int Power { get; init; }
    public required int Crew { get; init; }
    public int Slots { get; init; }
}