namespace Wolfe.SpaceTraders.Domain.Exploration;

[StronglyTypedId]
public partial struct WaypointTraitId
{
    public static readonly WaypointTraitId Marketplace = new("MARKETPLACE");
    public static readonly WaypointTraitId Shipyard = new("SHIPYARD");
}