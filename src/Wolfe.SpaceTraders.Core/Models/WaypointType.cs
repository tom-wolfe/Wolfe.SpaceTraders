namespace Wolfe.SpaceTraders.Core.Models;

[StronglyTypedId]
public partial struct WaypointType
{
    public static WaypointType Shipyard => new("SHIPYARD");
    public static WaypointType OrbitalStation => new("ORBITAL_STATION");
}