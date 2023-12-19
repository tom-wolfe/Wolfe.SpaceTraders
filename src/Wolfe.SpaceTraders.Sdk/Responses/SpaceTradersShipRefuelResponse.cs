using Wolfe.SpaceTraders.Sdk.Models;
using Wolfe.SpaceTraders.Sdk.Models.Ships;

namespace Wolfe.SpaceTraders.Sdk.Responses;

public class SpaceTradersShipRefuelResponse
{
    public required SpaceTradersAgent Agent { get; set; }
    public required SpaceTradersShipFuel Fuel { get; set; }
    public required SpaceTradersTransaction Transaction { get; set; }
}