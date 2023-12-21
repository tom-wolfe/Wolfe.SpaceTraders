using Wolfe.SpaceTraders.Sdk.Models.Navigation;

namespace Wolfe.SpaceTraders.Sdk.Models.Ships;

public class SpaceTradersShipNavigateResult
{
    public required SpaceTradersShipFuel Fuel { get; set; }
    public required SpaceTradersNavigation Nav { get; set; }
}