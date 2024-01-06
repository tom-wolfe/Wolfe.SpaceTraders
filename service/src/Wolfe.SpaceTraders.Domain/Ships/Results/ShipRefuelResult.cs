using Wolfe.SpaceTraders.Domain.General;

namespace Wolfe.SpaceTraders.Domain.Ships.Results;

public class ShipRefuelResult
{
    public required Fuel NewValue { get; init; }
    public required Credits Cost { get; init; }
}