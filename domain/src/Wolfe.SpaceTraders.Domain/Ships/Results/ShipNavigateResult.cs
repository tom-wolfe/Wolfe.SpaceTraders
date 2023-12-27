namespace Wolfe.SpaceTraders.Domain.Ships.Results;

public class ShipNavigateResult
{
    public required ShipFuel Fuel { get; set; }
    public required Navigation.Navigation Navigation { get; set; }
}