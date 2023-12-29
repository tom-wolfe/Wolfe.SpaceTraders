namespace Wolfe.SpaceTraders.Domain.Exploration;

[StronglyTypedId]
public partial struct WaypointId
{
    public SystemId System => new(Value[..Value.LastIndexOf('-')]);
}