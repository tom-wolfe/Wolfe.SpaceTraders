using Wolfe.SpaceTraders.Core.Models;

namespace Wolfe.SpaceTraders.Core.Responses;

public class PurchaseShipResponse
{
    public required Agent Agent { get; set; }
    public required Ship Ship { get; set; }
    public required ShipyardTransaction Transaction { get; set; }
}