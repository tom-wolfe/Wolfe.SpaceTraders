using Wolfe.SpaceTraders.Sdk.Models.Contracts;
using Wolfe.SpaceTraders.Sdk.Models.Ships;

namespace Wolfe.SpaceTraders.Sdk.Models;

public class SpaceTradersRegistration
{
    public required SpaceTradersAgent Agent { get; set; }
    public required SpaceTradersContract Contract { get; set; }
    public required SpaceTradersFaction Faction { get; set; }
    public required SpaceTradersShip Ship { get; set; }
    public required string Token { get; set; }
}