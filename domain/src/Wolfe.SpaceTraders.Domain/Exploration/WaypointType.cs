namespace Wolfe.SpaceTraders.Domain.Exploration;

[StronglyTypedId]
public partial struct WaypointType
{
    public static WaypointType Shipyard => new("SHIPYARD");
    public static WaypointType OrbitalStation => new("ORBITAL_STATION");
}