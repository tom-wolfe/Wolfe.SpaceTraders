namespace Wolfe.SpaceTraders.Domain.Exploration;

[StronglyTypedId]
public partial struct WaypointId
{
    /// <summary>
    /// Gets the system id of the waypoint.
    /// </summary>
    public SystemId System => new(Value[..Value.LastIndexOf('-')]);
}