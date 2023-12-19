using Wolfe.SpaceTraders.Sdk.Models;
using Wolfe.SpaceTraders.Sdk.Models.Ships;
using Wolfe.SpaceTraders.Sdk.Models.Shipyards;

namespace Wolfe.SpaceTraders.Sdk.Responses;

public class SpaceTradersPurchaseShipResponse
{
    public required SpaceTradersAgent Agent { get; set; }
    public required SpaceTradersShip Ship { get; set; }
    public required SpaceTradersShipyardTransaction Transaction { get; set; }
}