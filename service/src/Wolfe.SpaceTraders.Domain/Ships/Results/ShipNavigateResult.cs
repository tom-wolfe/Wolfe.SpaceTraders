namespace Wolfe.SpaceTraders.Domain.Ships.Results;

public class ShipNavigateResult
{
    public required Fuel FuelRemaining { get; init; }
    public required ShipNavigationDestination Destination { get; init; }
}