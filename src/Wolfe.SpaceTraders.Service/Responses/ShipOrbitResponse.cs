using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Domain.Models;

namespace Wolfe.SpaceTraders.Service.Responses;

public class ShipOrbitResponse
{
    public required Navigation Navigation { get; set; }
}