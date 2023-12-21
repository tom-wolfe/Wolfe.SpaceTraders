using Wolfe.SpaceTraders.Domain.Models;

namespace Wolfe.SpaceTraders.Service.Results;

public class PurchaseShipResult
{
    public required Agent Agent { get; set; }
    public required Ship Ship { get; set; }
    public required ShipyardTransaction Transaction { get; set; }
}