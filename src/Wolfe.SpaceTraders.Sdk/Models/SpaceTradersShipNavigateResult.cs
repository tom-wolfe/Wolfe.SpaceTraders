using Wolfe.SpaceTraders.Sdk.Models.Navigation;
using Wolfe.SpaceTraders.Sdk.Models.Ships;

namespace Wolfe.SpaceTraders.Sdk.Models;

public class SpaceTradersShipNavigateResult
{
    public required SpaceTradersShipFuel Fuel { get; set; }
    public required SpaceTradersNavigation Nav { get; set; }
}