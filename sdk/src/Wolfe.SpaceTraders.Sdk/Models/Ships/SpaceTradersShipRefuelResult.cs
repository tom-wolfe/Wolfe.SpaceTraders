using Wolfe.SpaceTraders.Sdk.Models.Agents;
using Wolfe.SpaceTraders.Sdk.Models.Marketplace;

namespace Wolfe.SpaceTraders.Sdk.Models.Ships;

public class SpaceTradersShipRefuelResult
{
    public required SpaceTradersAgent Agent { get; set; }
    public required SpaceTradersShipFuel Fuel { get; set; }
    public required SpaceTradersTransaction Transaction { get; set; }
}