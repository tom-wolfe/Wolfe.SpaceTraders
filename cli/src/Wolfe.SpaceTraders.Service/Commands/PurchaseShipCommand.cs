﻿using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Service.Commands;

public class PurchaseShipCommand
{
    public required ShipType ShipType { get; set; }
    public required WaypointSymbol WaypointSymbol { get; set; }
}