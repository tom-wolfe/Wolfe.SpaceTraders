using Wolfe.SpaceTraders.Sdk.Models.Navigation;

namespace Wolfe.SpaceTraders.Sdk.Responses;

public class SpaceTradersShipOrbitResponse
{
    public required SpaceTradersNavigation Nav { get; set; }
}