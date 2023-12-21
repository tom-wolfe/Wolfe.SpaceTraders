using Wolfe.SpaceTraders.Sdk.Models.Ships;

namespace Wolfe.SpaceTraders.Sdk.Responses;

public class SpaceTradersGetShipResponse
{
    public required SpaceTradersShip Data { get; set; }
}