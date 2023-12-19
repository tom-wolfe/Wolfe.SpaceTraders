using Wolfe.SpaceTraders.Sdk.Models.Navigation;

namespace Wolfe.SpaceTraders.Sdk.Models.Ships;

public class SpaceTradersShip
{
    public required string Symbol { get; set; }
    public required SpaceTradersNavigation Nav { get; set; }
}