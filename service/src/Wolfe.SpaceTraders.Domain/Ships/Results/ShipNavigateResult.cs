namespace Wolfe.SpaceTraders.Domain.Ships.Results;

public class ShipNavigateResult
{
    public required IShipFuelBase Fuel { get; init; }
    public required IShipNavigation Navigation { get; init; }
}