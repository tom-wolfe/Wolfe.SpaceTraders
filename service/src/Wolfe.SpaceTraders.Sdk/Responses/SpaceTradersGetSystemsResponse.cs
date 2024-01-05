using Wolfe.SpaceTraders.Sdk.Models;
using Wolfe.SpaceTraders.Sdk.Models.Systems;

namespace Wolfe.SpaceTraders.Sdk.Responses;

public class SpaceTradersGetSystemsResponse : ISpaceTradersListResponse<SpaceTradersSystem>
{
    public required IEnumerable<SpaceTradersSystem> Data { get; set; }
    public required SpaceTradersListMeta Meta { get; set; }
}