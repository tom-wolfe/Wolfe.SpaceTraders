namespace Wolfe.SpaceTraders.Domain.Ships;

[StronglyTypedId]
public partial struct ShipRole
{
    public static readonly ShipRole Fabricator = new("FABRICATOR");
    public static readonly ShipRole Harvester = new("HARVESTER");
    public static readonly ShipRole Hauler = new("HAULER");
    public static readonly ShipRole Interceptor = new("INTERCEPTOR");
    public static readonly ShipRole Excavator = new("EXCAVATOR");
    public static readonly ShipRole Transport = new("TRANSPORT");
    public static readonly ShipRole Repair = new("REPAIR");
    public static readonly ShipRole Surveyor = new("SURVEYOR");
    public static readonly ShipRole Command = new("COMMAND");
    public static readonly ShipRole Carrier = new("CARRIER");
    public static readonly ShipRole Patrol = new("PATROL");
    public static readonly ShipRole Satellite = new("SATELLITE");
    public static readonly ShipRole Explorer = new("EXPLORER");
    public static readonly ShipRole Refinery = new("REFINERY");
}