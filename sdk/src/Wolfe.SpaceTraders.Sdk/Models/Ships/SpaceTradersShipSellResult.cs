using Wolfe.SpaceTraders.Sdk.Models.Agents;
using Wolfe.SpaceTraders.Sdk.Models.Marketplace;

namespace Wolfe.SpaceTraders.Sdk.Models.Ships;

public class SpaceTradersShipSellResult
{
    public required SpaceTradersAgent Agent { get; set; }
    public required SpaceTradersShipCargo Cargo { get; set; }
    public required SpaceTradersTransaction Transaction { get; set; }
}