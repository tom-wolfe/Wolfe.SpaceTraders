using Wolfe.SpaceTraders.Domain.Models;

namespace Wolfe.SpaceTraders.Service.Responses;

public class ShipDockResponse
{
    public required Navigation Navigation { get; set; }
}