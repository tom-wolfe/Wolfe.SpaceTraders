using Wolfe.SpaceTraders.Domain.Models.Navigation;
using Wolfe.SpaceTraders.Domain.Models.Ships;

namespace Wolfe.SpaceTraders.Service.Results;

public class ShipNavigateResult
{
    public required ShipFuel Fuel { get; set; }
    public required Navigation Navigation { get; set; }
}