namespace Wolfe.SpaceTraders.Domain.Exploration;

[StronglyTypedId]
public readonly partial struct WaypointType
{
    public static readonly WaypointType Planet = new("PLANET");
    public static readonly WaypointType GasGiant = new("GAS_GIANT");
    public static readonly WaypointType Moon = new("MOON");
    public static readonly WaypointType OrbitalStation = new("ORBITAL_STATION");
    public static readonly WaypointType JumpGate = new("JUMP_GATE");
    public static readonly WaypointType AsteroidField = new("ASTEROID_FIELD");
    public static readonly WaypointType Asteroid = new("ASTEROID");
    public static readonly WaypointType EngineeredAsteroid = new("ENGINEERED_ASTEROID");
    public static readonly WaypointType AsteroidBase = new("ASTEROID_BASE");
    public static readonly WaypointType Nebula = new("NEBULA");
    public static readonly WaypointType DebrisField = new("DEBRIS_FIELD");
    public static readonly WaypointType GravityWell = new("GRAVITY_WELL");
    public static readonly WaypointType ArtificialGravityWell = new("ARTIFICIAL_GRAVITY_WELL");
    public static readonly WaypointType FuelStation = new("FUEL_STATION");
}