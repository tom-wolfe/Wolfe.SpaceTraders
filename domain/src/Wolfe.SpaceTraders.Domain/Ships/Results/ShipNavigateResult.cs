namespace Wolfe.SpaceTraders.Domain.Ships.Results;

public class ShipNavigateResult
{
    public required ShipFuel Fuel { get; init; }
    public required Navigation.Navigation Navigation { get; init; }
}