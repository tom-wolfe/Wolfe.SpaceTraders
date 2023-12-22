using Wolfe.SpaceTraders.Sdk.Models;
using Wolfe.SpaceTraders.Sdk.Models.Systems;

namespace Wolfe.SpaceTraders.Sdk.Responses;

public class SpaceTradersGetWaypointsResponse : ISpaceTradersListResponse<SpaceTradersWaypoint>
{
    public required IEnumerable<SpaceTradersWaypoint> Data { get; set; }
    public required SpaceTradersListMeta Meta { get; set; }
}