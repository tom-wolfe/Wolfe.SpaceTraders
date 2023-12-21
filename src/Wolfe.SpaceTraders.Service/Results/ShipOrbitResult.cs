using Wolfe.SpaceTraders.Domain.Models.Navigation;

namespace Wolfe.SpaceTraders.Service.Results;

public class ShipOrbitResult
{
    public required Navigation Navigation { get; set; }
}