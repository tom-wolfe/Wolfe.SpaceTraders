using Wolfe.SpaceTraders.Domain.Models.Extraction;
using Wolfe.SpaceTraders.Domain.Models.Ships;

namespace Wolfe.SpaceTraders.Service.Results;

public class ShipExtractResult
{
    public required ShipCooldown Cooldown { get; set; }
    public required ExtractionYield Yield { get; set; }
    public required ShipCargo Cargo { get; set; }
}