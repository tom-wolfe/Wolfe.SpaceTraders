﻿namespace Wolfe.SpaceTraders.Sdk.Requests;

public class SpaceTradersPurchaseShipRequest
{
    public required string ShipType { get; set; }
    public required string Waypoint { get; set; }
}