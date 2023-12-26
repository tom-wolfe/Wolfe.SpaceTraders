using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Domain.Shipyards;

namespace Wolfe.SpaceTraders.Service.Results;

public class PurchaseShipResult
{
    public required Agent Agent { get; set; }
    public required Ship Ship { get; set; }
    public required ShipyardTransaction Transaction { get; set; }
}