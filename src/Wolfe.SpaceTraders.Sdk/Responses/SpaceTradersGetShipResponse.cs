using Wolfe.SpaceTraders.Sdk.Models;
using Wolfe.SpaceTraders.Sdk.Models.Ships;

namespace Wolfe.SpaceTraders.Sdk.Responses;

public class SpaceTradersGetShipsResponse : ISpaceTradersListResponse<SpaceTradersShip>
{
    public required IEnumerable<SpaceTradersShip> Data { get; set; }
    public required SpaceTradersListMeta Meta { get; set; }
}