using Wolfe.SpaceTraders.Sdk.Models.Ships;

namespace Wolfe.SpaceTraders.Sdk.Models.Extraction;

public class SpaceTradersShipExtractResult
{
    public required SpaceTradersShipCooldown Cooldown { get; set; }
    public required SpaceTradersExtraction Extraction { get; set; }
    public required SpaceTradersShipCargo Cargo { get; set; }
}