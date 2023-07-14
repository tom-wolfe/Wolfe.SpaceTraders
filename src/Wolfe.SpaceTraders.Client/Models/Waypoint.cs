﻿namespace Wolfe.SpaceTraders.Models;

public class Waypoint
{
    public required WaypointSymbol Symbol { get; set; }
    public required WaypointType Type { get; set; }
    public required SystemSymbol SystemSymbol { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public List<ShallowWaypoint> Orbitals { get; } = new();
    public required ShallowFaction Faction { get; set; }
    public required Chart Chart { get; set; }
}