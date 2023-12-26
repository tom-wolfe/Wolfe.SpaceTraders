using Wolfe.SpaceTraders.Domain.Extraction;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Service.Results;

public class ShipExtractResult
{
    public required ShipCooldown Cooldown { get; set; }
    public required ExtractionYield Yield { get; set; }
    public required ShipCargo Cargo { get; set; }
}