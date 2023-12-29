namespace Wolfe.SpaceTraders.Domain.Exploration;

[StronglyTypedId]
public partial struct SystemType
{
    public static readonly SystemType NeutronStar = new("NEUTRON_STAR");
    public static readonly SystemType RedStar = new("RED_STAR");
    public static readonly SystemType OrangeStar = new("ORANGE_STAR");
    public static readonly SystemType BlueStar = new("BLUE_STAR");
    public static readonly SystemType YoungStar = new("YOUNG_STAR");
    public static readonly SystemType WhiteDwarf = new("WHITE_DWARF");
    public static readonly SystemType BlackHole = new("BLACK_HOLE");
    public static readonly SystemType Hypergiant = new("HYPERGIANT");
    public static readonly SystemType Nebula = new("NEBULA");
    public static readonly SystemType Unstable = new("UNSTABLE");
}