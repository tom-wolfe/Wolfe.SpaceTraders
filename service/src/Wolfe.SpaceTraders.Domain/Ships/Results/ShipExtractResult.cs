namespace Wolfe.SpaceTraders.Domain.Ships.Results;

public class ShipExtractResult
{
    public required ShipCooldown Cooldown { get; init; }
    public required ShipCargoItem Yield { get; init; }
}