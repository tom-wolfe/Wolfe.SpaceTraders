using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Domain.Shipyards;

namespace Wolfe.SpaceTraders.Domain.Fleet.Results;

public class PurchaseShipResult
{
    public required Agent Agent { get; init; }
    public required Ship Ship { get; init; }
    public required ShipyardTransaction Transaction { get; init; }
}