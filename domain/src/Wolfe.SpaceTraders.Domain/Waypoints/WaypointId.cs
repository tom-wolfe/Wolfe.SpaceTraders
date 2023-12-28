using Wolfe.SpaceTraders.Domain.Systems;

namespace Wolfe.SpaceTraders.Domain.Waypoints;

[StronglyTypedId]
public partial struct WaypointId
{
    public SystemId System => new(Value[..Value.LastIndexOf('-')]);
}