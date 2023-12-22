using Wolfe.SpaceTraders.Sdk.Models.Ships;

namespace Wolfe.SpaceTraders.Sdk.Responses;

public class SpaceTradersPatchShipNavResponse
{
    public required SpaceTradersPatchShipNavResult Data { get; set; }
}