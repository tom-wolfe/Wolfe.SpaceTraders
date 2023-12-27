using Wolfe.SpaceTraders.Domain.Extraction;

namespace Wolfe.SpaceTraders.Domain.Ships.Results;

public class ShipExtractResult
{
    public required ShipCooldown Cooldown { get; set; }
    public required ExtractionYield Yield { get; set; }
    public required ShipCargo Cargo { get; set; }
}