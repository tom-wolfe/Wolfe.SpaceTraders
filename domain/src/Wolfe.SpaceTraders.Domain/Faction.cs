﻿using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Domain;

public class Faction
{
    public FactionId Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required WaypointId Headquarters { get; init; }
}