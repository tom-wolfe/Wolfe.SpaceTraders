﻿using Wolfe.SpaceTraders.Sdk.Models;

namespace Wolfe.SpaceTraders.Sdk.Responses;

public class SpaceTradersGetWaypointsResponse : ISpaceTradersListResponse<SpaceTradersWaypoint>
{
    public required IEnumerable<SpaceTradersWaypoint> Data { get; set; }
    public required SpaceTradersListMeta Meta { get; set; }
}