namespace Wolfe.SpaceTraders.Domain.Waypoints;

[StronglyTypedId]
public partial struct WaypointType
{
    public static WaypointType Shipyard => new("SHIPYARD");
    public static WaypointType OrbitalStation => new("ORBITAL_STATION");
}