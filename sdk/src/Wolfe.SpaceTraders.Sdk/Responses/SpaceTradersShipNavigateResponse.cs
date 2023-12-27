using Wolfe.SpaceTraders.Sdk.Models.Navigation;

namespace Wolfe.SpaceTraders.Sdk.Responses;

public class SpaceTradersPatchShipNavResponse
{
    public required SpaceTradersNavigation Data { get; set; }
}