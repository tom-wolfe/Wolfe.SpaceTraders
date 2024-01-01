namespace Wolfe.SpaceTraders.Domain.Ships.Results;

public class ShipExtractResult
{
    public required ShipCooldown Cooldown { get; init; }
    public required ExtractionYield Yield { get; init; }
    public required ShipCargo Cargo { get; init; }
}