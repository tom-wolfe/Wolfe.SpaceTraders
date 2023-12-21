﻿namespace Wolfe.SpaceTraders.Domain.Models;

public class Waypoint
{
    public required WaypointSymbol Symbol { get; set; }
    public required WaypointType Type { get; set; }
    public required SystemSymbol SystemSymbol { get; set; }
    public required Location Location { get; set; }
}