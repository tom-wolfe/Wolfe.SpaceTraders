using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Marketplace;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Service.Results;

public class ShipSellResult
{
    public required Agent Agent { get; set; }
    public required ShipCargo Cargo { get; set; }
    public required Transaction Transaction { get; set; }
}