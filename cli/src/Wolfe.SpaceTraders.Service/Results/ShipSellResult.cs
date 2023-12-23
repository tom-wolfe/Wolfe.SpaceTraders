using Wolfe.SpaceTraders.Domain.Models.Agents;
using Wolfe.SpaceTraders.Domain.Models.Marketplace;
using Wolfe.SpaceTraders.Domain.Models.Ships;

namespace Wolfe.SpaceTraders.Service.Results;

public class ShipSellResult
{
    public required Agent Agent { get; set; }
    public required ShipCargo Cargo { get; set; }
    public required Transaction Transaction { get; set; }
}