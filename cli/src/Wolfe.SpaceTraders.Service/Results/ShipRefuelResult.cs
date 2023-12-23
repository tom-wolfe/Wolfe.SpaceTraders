using Wolfe.SpaceTraders.Domain.Models.Agents;
using Wolfe.SpaceTraders.Domain.Models.Marketplace;
using Wolfe.SpaceTraders.Domain.Models.Ships;

namespace Wolfe.SpaceTraders.Service.Results;

public class ShipRefuelResult
{
    public required Agent Agent { get; set; }
    public required ShipFuel Fuel { get; set; }
    public required Transaction Transaction { get; set; }
}